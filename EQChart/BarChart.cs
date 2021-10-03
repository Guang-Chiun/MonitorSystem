using System;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace EQChart
{
    public partial class BarChart : UserControl, IEQChart
    {
        public BarChart()
        {
            InitializeComponent();
            InitialChart();
        }

        public Func<string, DataTable> SQLFunc = null;

        private Chart chart1 = new Chart();
        private ChartArea chartArea1 = new ChartArea();
        private Legend legend1 = new Legend();
        private Series series1 = new Series();   //直方圖一個chart就好
        private HitTestResult HitTestResult = new HitTestResult();   //點擊bar觸發事件
        private DataTable chartDataTable = null;
        private Dictionary<string, int> itemBoundaryDic = new Dictionary<string, int>();

        //Net Core無法直接於UI上設計Chart物件，要透過程式碼實作
        private void InitialChart(string Title = null)
        {
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Size = this.Size;
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Chocolate;
            series1.ChartArea = "ChartArea1";
            series1.Name = "GlassNum";
            series1.BorderWidth = 2;
            this.chart1.Series.Add(series1);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            this.chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Enabled = false;
            this.chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            this.chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
            this.chart1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseClick);    //加入點擊事件

            this.Controls.Add(this.chart1);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
        }
    
        public void RefrechChart()
        {
            if (SQLFunc != null)
            {
                lbSelectedInfo.Text = "";
                string ColumnInfo = cbxMonitorItem.Text;
                chartDataTable = SQLFunc.Invoke(ColumnInfo);
                chart1.DataSource = chartDataTable;
                //chart1.Series["GlassNum"].YValueMembers = ColumnInfo;   //"MaskCommonDefect               
                int ColumnInfoErrorNum = this.itemBoundaryDic[ColumnInfo];
                chart1.Series["GlassNum"].Points.Clear();
                int num = chartDataTable.Rows.Count;
                //int num = chart1.Series["GlassNum"].Points.Count;
                int Yvalue = 0;
                for (int i = 0; i < num; i++)
                {
                    int index = num - i - 1;
                    object oValue = chartDataTable.Rows[index].ItemArray[1];
                    switch (oValue.GetType().Name)
                    {
                        case "Int32":
                            Yvalue = (int)oValue;
                            break;
                        case "Boolean":
                            Yvalue = (bool)oValue ? 1 : 0;
                            break;
                        default:
                            throw (new ApplicationException("型別不符"));
                    }
                    
                    chart1.Series["GlassNum"].Points.AddXY(i, Yvalue);
                    chart1.Series["GlassNum"].Points[i].Color = Yvalue >= ColumnInfoErrorNum ? Color.Red : Color.Green;                 
                }
                chart1.Refresh();
                Console.WriteLine("Refresh BarChart");
            }                  
        }

        private void cbxMonitorItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            string itemName = (string)comboBox.SelectedItem;
            this.tbBoundary.Text = itemBoundaryDic[itemName].ToString();
            this.RefrechChart();
        }

        private void chart1_MouseClick(object sender, MouseEventArgs e)
        {
            HitTestResult = this.chart1.HitTest(e.X, e.Y);
            if (HitTestResult.Series != null && chartDataTable != null)
            {
                int index = HitTestResult.PointIndex;   //點急的index
                if (index >= 0)
                {
                    int num = HitTestResult.Series.Points.Count;
                    string GlassName = chartDataTable.Rows[num - index - 1].ItemArray[0].ToString();
                    string Yvalue = chartDataTable.Rows[num - index - 1].ItemArray[1].ToString();
                                    
                    for (int i = 0; i < num; i++)
                        HitTestResult.Series.Points[i].BorderColor = HitTestResult.Series.Points[i].Color;
                    
                    HitTestResult.Series.Points[index].BorderColor = Color.Blue;
                    lbSelectedInfo.Text = $"GlassName : {GlassName} - Value : {Yvalue}";
                    lbSelectedInfo.ForeColor = Color.Blue;
                }
            }
        }

        public void SetMonitorItem(List<TBarChartMonitorItem> MonitorItems)
        {
            foreach (var item in MonitorItems)
            {
                this.cbxMonitorItem.Items.Add(item.itemName);
                this.itemBoundaryDic.Add(item.itemName, item.Boundary);
            }
            
            if (this.cbxMonitorItem.Items.Count > 0)
                this.cbxMonitorItem.SelectedIndex = 0;
        }

        /// <summary>
        /// 限定只能輸入數字以及backspace
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbBoundary_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 按下Enter改變範圍
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbBoundary_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    string currentItem = this.cbxMonitorItem.Text;
                    if (String.IsNullOrEmpty(currentItem))
                        return;
                    this.itemBoundaryDic[currentItem] = Convert.ToInt32(tbBoundary.Text);
                    this.RefrechChart();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }          
            }
        }      
    }
}
