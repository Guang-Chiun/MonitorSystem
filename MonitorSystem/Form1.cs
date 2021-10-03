using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;
using System.Data.SqlClient;
using MonitorSystem.DataBase;
using MonitorSystem.DataBaseMonitor;
using MonitorSystem.Connect;
using MonitorSystem.FactoryPattern;
using EQControl;
using EQChart;
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;
using System.Reflection;

namespace MonitorSystem
{
    public partial class FmMain : Form
    {
        #region Public Field
        DbConnection dbConnection;   //要有一個地方時時監控這個物件連線狀態
        List<IDataBaseTableMonitor> dataBaseTableMonitorList;       
        public static Queue<Glass> DataBaseUpdateInfoQueue;
        ServerSocket ServerSocket = new ServerSocket();
        BackGroundProcess backGroundProcess;
        Dictionary<string, EQControl.EQControl> EQControlUIDic = new Dictionary<string, EQControl.EQControl>();

        System.Drawing.Graphics formGraphics;
        Dictionary<string, List<TEQChartInfo>> EQChartUIDic = new Dictionary<string, List<TEQChartInfo>>();
        string CurrentSelectedEQID = String.Empty;

        Action<string> RefreshEQWarningInfo;


        #endregion

        #region Construct
        public FmMain()
        {
            InitializeComponent();

            formGraphics = this.CreateGraphics();
            dataBaseTableMonitorList = new List<IDataBaseTableMonitor>();
            DataBaseUpdateInfoQueue = new Queue<Glass>();

            //一開始要連接資料庫                      
            IDataBaseFactory factory = new SQLServerFactory();   //這裡要改寫成reflection(如果更換資料庫就不用改)
            string sConnectInfo = StaticSQLServerCommond.GetConnectionInformation();
            dbConnection = factory.CreateDataBaseConnect(sConnectInfo);
            dbConnection.Open();
            if (dbConnection.State == ConnectionState.Open)
            {
                Console.WriteLine("連線成功");
            }

            //監控表格部分，可監控多個表格，不同表格監控內容寫在不同類別裡面
            Task.Run(() => 
            {
                Thread.CurrentThread.Priority = ThreadPriority.Lowest;
                IDataBaseTableMonitor dataBaseTableMonitor = new MonitorSQLServerGlassTable();
                string sConn = StaticSQLServerCommond.GetConnectionInformation();
                dataBaseTableMonitorList.Add(dataBaseTableMonitor);
                foreach (var monitorTable in dataBaseTableMonitorList)
                {
                    monitorTable.StartMonitor(sConn);
                }

            });
            

            //2021-10-01 加入刷新事件
            RefreshEQWarningInfo = EQID =>
            {
                 var EQControl = EQControlUIDic[EQID];
                 if (EQControl.EQID == EQID && EQID == CurrentSelectedEQID)
                     rtbEQWarning.Text = EQControl.GetWarningInfo();
            };


            JsonFileOperate.ReadJsonEQInformation();
            this.InitialEQControl();

            //Task : 另外開執行序不斷監控Queue是否有資訊，如果有就依照指定資訊做事(EX : 刷新LOG)
            //要加速才會考慮用多個Task，確認Task結束之類的，如果只要刷新UI另外開一個Task Run即可
            Task.Run(() => 
            {
                Thread.CurrentThread.Priority = ThreadPriority.Lowest;
                this.CheckQueue();
                
            });
        }
        #endregion

        #region Private Function
        /// <summary>
        /// 初始化動態新增EQControl物件
        /// </summary>
        private void InitialEQControl()
        {
            foreach (var jsonEQInformation in JsonFileOperate.JsonEQInformationList)
            {
                EQControl.EQControl EQControlUI = new EQControl.EQControl();
                EQControlUI.Name = $"eqControl{jsonEQInformation.EQID}";
                EQControlUI.BackColor = System.Drawing.Color.Silver;
                EQControlUI.EQID = jsonEQInformation.EQID;
                EQControlUI.Password = jsonEQInformation.EQPassword;
                EQControlUI.EQName = jsonEQInformation.EQName;
                EQControlUI.Location = jsonEQInformation.EQPosition;
                EQControlUI.Size = jsonEQInformation.EQSize;
                EQControlUI.Click += new System.EventHandler(this.eqControl_Click);
                //2021-10-01
                EQControlUI.RefreshUI = RefreshEQWarningInfo;

                this.Controls.Add(EQControlUI);

                if (!EQControlUIDic.ContainsKey(EQControlUI.EQID))
                    EQControlUIDic.Add(EQControlUI.EQID, EQControlUI);

                //2021-10-01

                ConnectConst.AllowLogicEQList.Add(EQControlUI.EQID, EQControlUI.Password);
                this.SetEQWarningInfo(jsonEQInformation.EQID, jsonEQInformation.EQWarningInfoList);
                this.CreateEQChart(jsonEQInformation.EQID, jsonEQInformation.EQChartList);
            }

            backGroundProcess = new BackGroundProcess(ref EQControlUIDic);
        }

        /// <summary>
        /// 設定每台機檯警報資訊
        /// </summary>
        /// <param name="EQID"></param>
        /// <param name="jsonEQChartList"></param>
        private void SetEQWarningInfo(string EQID, IList<JsonEQWarningInfo> JsonEQWarningInfoList)
        {
            string sEQErrorMsg = String.Empty;
            foreach (var item in JsonEQWarningInfoList)
            {
                if (!WarningInformation.m_WarningInfoDic.ContainsKey(item.ID))
                    sEQErrorMsg = sEQErrorMsg.Insert(sEQErrorMsg.Length, $"{EQID} 沒有 {item.ID} 之警報ID\r\n");              
            }

            //若是json有設定錯誤直接中斷程式
            if (!String.IsNullOrEmpty(sEQErrorMsg))
            {
                MessageBox.Show(sEQErrorMsg);
                System.Environment.Exit(0);
            }
                
            WarningJudge.AddJsonEQWarningInfoToDic(EQID, JsonEQWarningInfoList);
        }

        /// <summary>
        /// 創建每台EQ會使用到的圖表函數
        /// </summary>
        /// <param name="jsonEQChartList"></param>
        private void CreateEQChart(string EQID, IList<JsonEQChart> jsonEQChartList)
        {
            string sEQErrorMsg = String.Empty;
            int index = 0;            
            foreach (var jsonEQChart in jsonEQChartList)
            {
                foreach (var item in jsonEQChart.MonitorItemList)    //檢查資料庫內監控項目
                {
                    string errorMsg = CheckValue.GetErrorMsg(item);
                    if (!String.IsNullOrEmpty(errorMsg))
                        sEQErrorMsg = sEQErrorMsg.Insert(sEQErrorMsg.Length, $"{EQID} MonitorItems: {errorMsg}");            
                }
                                            
                Assembly assembly = Assembly.Load("MonitorSystem");
                string sChartType = jsonEQChart.chartType;
                string chartFactoryClass = $"MonitorSystem.FactoryPattern.{sChartType}Factory";
                Type type = assembly.GetType(chartFactoryClass);            
                var CreateChartFactory = (IEQChartFactory)Activator.CreateInstance(type);
                IEQChart EQChart = CreateChartFactory.CreateEQChart(EQID, index, jsonEQChart);

                Control control = EQChart as Control;
                this.pnlChartInfo.Controls.Add(control);

                TEQChartInfo tEQChartInfo;
                tEQChartInfo.chartName = jsonEQChart.ShowChartName;
                tEQChartInfo.EQChart = EQChart;
                tEQChartInfo.ChartUI = control;
                if (!EQChartUIDic.ContainsKey(EQID))
                    EQChartUIDic.Add(EQID, new List<TEQChartInfo>() { tEQChartInfo });
                else
                    EQChartUIDic[EQID].Add(tEQChartInfo);

                EQChart.RefrechChart();
                index++;
            }

            //若是json有設定錯誤直接中斷程式
            if (!String.IsNullOrEmpty(sEQErrorMsg))
            {
                MessageBox.Show(sEQErrorMsg);              
                System.Environment.Exit(0);   
            }               
        }

        /// <summary>
        /// 檢查更新的Queue裡是否有資料
        /// </summary>
        private void CheckQueue()
        {
            while (true)
            {
                if (DataBaseUpdateInfoQueue.Count != 0)
                {                   
                    this.Invoke(new Action(() =>
                    {
                        Glass glass = DataBaseUpdateInfoQueue.Dequeue();
                        string EQID = "EQ" + glass.EQID;
                        int Type = JudgeGlassWarning(glass);
                        backGroundProcess.SetWarningInfo(EQID, Type);
                        string info = $"{DateTime.Now.ToString("hh:mm:ss")} : {EQID} 新增 {glass.GlassName} 資訊\r\n";
                        rtbUpdateInfo.Text = rtbUpdateInfo.Text.Insert(0, info);

                        //2021-09-26 有新增資料圖表要刷新
                        foreach (var tEQChartInfo in EQChartUIDic[EQID])
                            tEQChartInfo.EQChart.RefrechChart();
                    }));
                }

                //2021-08-16 機台登錄上線
                if (ConnectConst.EQRunningLog.Count != 0)
                {
                    this.Invoke(new Action(() =>
                    {
                        TEQRunningStatus tEQRunningStatus = ConnectConst.EQRunningLog.Dequeue();                   
                        string EQID = tEQRunningStatus.EQID;
                        bool bRunning = tEQRunningStatus.Running;
                        Console.WriteLine($"REFRESH EQID : {EQID}, Running : {bRunning}");
                        backGroundProcess.SetRunningInfo(EQID, tEQRunningStatus.Running);
                        string sRunnungStatus = bRunning ? "上線" : "離線";
                        string info = $"{DateTime.Now.ToString("hh:mm:ss")} : 機台:{EQID}, {sRunnungStatus}\r\n";                    
                        rtbUpdateInfo.Text = rtbUpdateInfo.Text.Insert(0, info);
                    }));                                  
                }

                //2021-08-14 檢查client傳來的警報訊息
                if (ConnectConst.clientWarningQueue.Count != 0)
                {                   
                    this.Invoke(new Action(() =>
                    {
                        TWarningInfoFromClient warningInfoFromClient = ConnectConst.clientWarningQueue.Dequeue();
                        string EQID = warningInfoFromClient.EQID;
                        string WarningMsg = warningInfoFromClient.WarningMessage;
                        int Type = WarningJudge.JudgeClientEQWarning(ref WarningMsg);
                        backGroundProcess.SetWarningInfo(EQID, Type);
                        string info = $"{warningInfoFromClient.errorTime.ToString("hh:mm:ss")} : 機台:{EQID}, 發生警報:{WarningMsg}\r\n";
                        rtbUpdateInfo.Text = rtbUpdateInfo.Text.Insert(0, info);
                    }));
                }

                Thread.Sleep(1);
            }         
        }

        

        /// <summary>
        /// 判斷玻璃資訊警報，產品寫在外面，警報類型封裝起來
        /// </summary>
        /// <param name="glass"></param>
        /// <returns></returns>
        private int JudgeGlassWarning(Glass glass)
        {
            int result = 0;
            string EQID = $"EQ{glass.EQID}";
            result |= WarningJudge.JudgeDefectOverflow(EQID, Convert.ToInt32(glass.DefectNum));
            result |= WarningJudge.JudgeMaskCommon(EQID, glass.MaskCommonDefect == "1");
            result |= WarningJudge.JudgeGlassCommon(EQID, glass.GlassCommonDefect == "1");
            return result;
        }
        #endregion

        #region UI Operate Function
        /// <summary>
        /// 點選機台控制項時，畫上邊框，這樣才知道目前點選的
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eqControl_Click(object sender, EventArgs e)
        {
            EQControl.EQControl EQControlselected = (EQControl.EQControl)sender;
            string ID = EQControlselected.EQID;
            string EQName = EQControlselected.EQName;
            lbEQWarningInfo.Text = $"{EQName} : Warning Messages";
            formGraphics.Clear(this.BackColor);
            System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Green, 8);
            formGraphics.DrawRectangle(myPen, new Rectangle(EQControlselected.Location, EQControlselected.Size));
            rtbEQWarning.Clear();
            rtbEQWarning.Text = EQControlselected.GetWarningInfo();
            myPen.Dispose();

            //2021-09-27 加入圖表顯示切換相關
            CurrentSelectedEQID = ID;
            this.DefaultShowEQChart(CurrentSelectedEQID);
        }

        private void SetComboBox(string EQID)
        {
            cbxShowChart1.Items.Clear();
            cbxShowChart2.Items.Clear();

            //先所有EQChart都隱藏
            foreach (KeyValuePair<string, List<TEQChartInfo>> kvp in EQChartUIDic)
                foreach (var EQChartInfo in EQChartUIDic[kvp.Key])
                    EQChartInfo.ChartUI.Visible = false;

            foreach (var EQChartInfo in EQChartUIDic[EQID])
            {
                //加入顯示圖表名稱
                cbxShowChart1.Items.Add(EQChartInfo.chartName);   
                cbxShowChart2.Items.Add(EQChartInfo.chartName);
            }
        }

        /// <summary>
        /// 預設顯示EQChart圖表
        /// </summary>
        /// <param name="CurrentSelectedEQID"></param>
        private void DefaultShowEQChart(string CurrentSelectedEQID)
        {
            this.SetComboBox(CurrentSelectedEQID);
            if (EQChartUIDic[CurrentSelectedEQID].Count > 1)
            {            
                this.cbxShowChart2.Visible = true;
                this.cbxShowChart1.SelectedIndex = 0;
                this.cbxShowChart2.SelectedIndex = 1;
            }
            else if (EQChartUIDic[CurrentSelectedEQID].Count == 1)
            {
                this.cbxShowChart2.Visible = false;
                this.cbxShowChart1.SelectedIndex = 0;
            }
        }

        private void cbxShowChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(CurrentSelectedEQID))
                return;
            ComboBox comboBox = (ComboBox)sender;
            int SelectedIndex = comboBox.SelectedIndex;
            EQChartUIDic[CurrentSelectedEQID][SelectedIndex].ChartUI.Location = new Point(comboBox.Location.X, comboBox.Location.Y + 30);
            EQChartUIDic[CurrentSelectedEQID][SelectedIndex].ChartUI.Visible = true;
            EQChartUIDic[CurrentSelectedEQID][SelectedIndex].ChartUI.BringToFront();
        }

        #endregion
    }
}