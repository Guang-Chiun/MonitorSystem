using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace EQControl
{
    public partial class EQControl : UserControl
    {
        /// <summary>
        /// 給定機台ID
        /// </summary>
        public string EQID
        {
            get { return this.lbEQID.Text; }
            set { this.lbEQID.Text = value; }
        }

        /// <summary>
        /// 給定機台登錄密碼
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 機台名稱
        /// </summary>
        public string EQName
        {
            get { return this.lbEQName.Text; }
            set { this.lbEQName.Text = value; }
        }

        public Action<string> RefreshUI;

        //可加入stack記錄發生錯誤訊息的時間
        private List<string> WarningList = new List<string>();
        //目前累加的Type
        private int OverlapWarningType = 0;
        //2021-08-16 機台是否運作中
        private bool bRunning;
     
        public EQControl()
        {
            InitializeComponent();
            this.btnReset.Location = this.Location;
        }
      
        /// <summary>
        /// 取得目前發生了那些警報
        /// </summary>
        /// <returns></returns>
        public string GetWarningInfo()
        {
            string sInfo = "";
            int Count = WarningList.Count;
            for (int i = Count - 1; i >= 0; i--)
            {
                sInfo += WarningList[i] + "\r\n";
            }
            return sInfo;          
        }

        //這個其實可以寫在外部
        //sender 是 backgroundProcess(哪個物件引起了這個事件?) 應該是有一個連線物件(連線，才觸發這個事件，這樣看起來比較合理)
        //Connect Object => 有一個Refresh UI...
        /// <summary>
        /// 若是成功連上線顯示為亮綠色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RefreshRunningStatus(object sender, EventArgs e)
        {
            EQRunningEventArgs eqRunningEventArgs = e as EQRunningEventArgs;
            if (eqRunningEventArgs.EQID == this.EQID)
            {
                bRunning = eqRunningEventArgs.Running;
                //2021-09-28 只要機台不在線一律顯示灰色
                if (!bRunning)
                    this.BackColor = Color.Silver;             
                else
                    this.BackColor = GetWarningBackColor(OverlapWarningType);
            }           
        }

        /// <summary>
        /// 知道有那些警報類型
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        private List<int> GetWarningTypeList(int Type)
        {
            string bin = Convert.ToString(Type, 2);
            char[] charArray = bin.ToCharArray();
            List<int> Warning = new List<int>();
            for (int i = charArray.Length - 1; i >= 0; i--)
            {
                if (charArray[i] == '1')
                {
                    int power = charArray.Length - i - 1;
                    int value = (int)Math.Pow(2, power);
                    Warning.Add(value);
                }
            }
            return Warning;
        }

        /// <summary>
        /// 依照警告類型刷新UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RefreshWarningStatus(object sender, EventArgs e)
        {
            WarningEventArgs warningEventArgs = e as WarningEventArgs;
            if (warningEventArgs.EQID == this.EQID)
            {
                OverlapWarningType |= warningEventArgs.Type;
                int Type = warningEventArgs.Type;
                //EWarningSeverity eWarningSeverity = EWarningSeverity.OK;
                foreach (var item in GetWarningTypeList(Type))
                {
                    if (WarningInformation.m_WarningInfoDic.ContainsKey(item))
                    {
                        string Warning = WarningInformation.m_WarningInfoDic[item];                       
                        WarningList.Add($"{DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")} : {Warning}");
                    }                  
                }
                this.BackColor = GetWarningBackColor(OverlapWarningType);
                //2021-10-01 Refresh
                this.RefreshUI.Invoke(EQID);
            }           
        }

        private Color GetWarningBackColor(int OverlapWarningType)
        {
            Color backColor = new Color();
            if (OverlapWarningType == 0)
            {
                backColor = bRunning ? Color.Lime : Color.Silver;
            }

            //嚴重性要看累加的Warning
            EWarningSeverity eWarningSeverity = EWarningSeverity.OK;
            foreach (var item in GetWarningTypeList(OverlapWarningType))
            {
                if (WarningInformation.m_WarningSeverityDic.ContainsKey(item))
                {
                    EWarningSeverity eWarning = WarningInformation.m_WarningSeverityDic[item];
                    if ((int)eWarning > (int)eWarningSeverity)
                        eWarningSeverity = eWarning;
                }
            }

            switch (eWarningSeverity)
            {
                case EWarningSeverity.RED:
                    this.btnReset.Visible = true;
                    backColor = Color.Red;
                    break;
                case EWarningSeverity.YELLOW:
                    this.btnReset.Visible = true;
                    backColor = Color.Yellow;
                    break;
                default:
                    break;
            }
            return backColor;
        }


        private void btnReset_Click(object sender, EventArgs e)
        {
            //要注意彈出視窗會不會卡程式
            DialogResult result = MessageBox.Show($"確認要清除{this.EQName}警報?", "消除警報確認", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                OverlapWarningType = 0;
                WarningList.Clear();
                this.RefreshUI(EQID);
                this.BackColor = bRunning ? Color.Lime : Color.Silver;
                this.btnReset.Visible = false;
            }
            else
            {
                return;
            }
        }
    }
}
