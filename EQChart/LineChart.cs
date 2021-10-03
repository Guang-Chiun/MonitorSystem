using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace EQChart
{
    public partial class LineChart : UserControl, IEQChart
    {
        //2021-09-21 最大限制先寫在這裡
        const int MAX_LINECHART_SERIES_NUM = 4;
        Color[] lineChartSeriesColor = new Color[]
        {
            Color.Red, Color.Green, Color.Blue, Color.Brown
        };

        //2021-09-21 給定開始與結束時間查詢
        public Func<DateTime, DateTime, DataTable> SQLFunc = null;
        private DataTable chartDataTable = null;

        private Chart chart1 = new Chart();
        private ChartArea chartArea2 = new ChartArea();
        private Legend legend2 = new Legend();

        private bool bShowLast1Hour;
        private DateTime startTime;
        private DateTime endTime;

        public LineChart()
        {
            InitializeComponent();
            InitialChart();
            this.chart1.Size = new Size(this.Width, this.Height - 25);
        }

        private void InitialChart(string Title = null)
        {
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Alignment = System.Drawing.StringAlignment.Center;
            legend2.AutoFitMinFontSize = 5;
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend2.LegendItemOrder = System.Windows.Forms.DataVisualization.Charting.LegendItemOrder.ReversedSeriesOrder;
            legend2.Name = "Legend1";
            legend2.Position.Auto = false;
            legend2.Position.Height = 20F;  //20F 15
            legend2.Position.Width = 100F;   //65F 100
            legend2.Position.X = 0F; //35F
            legend2.TitleForeColor = System.Drawing.Color.Maroon;

            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Excel;
            this.chart1.Size = new System.Drawing.Size(553, 262);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";

            chart1.ChartAreas["ChartArea1"].Position.Auto = false;
            chart1.ChartAreas["ChartArea1"].Position.X = 10;
            chart1.ChartAreas["ChartArea1"].Position.Y = 20;
            chart1.ChartAreas["ChartArea1"].Position.Width = 95;
            chart1.ChartAreas["ChartArea1"].Position.Height = 75;

            this.chart1.ChartAreas["ChartArea1"].AxisY.IsStartedFromZero = false;
            this.chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            this.chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
            this.chart1.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            //chart1.ChartAreas[0].AxisY.LabelStyle.Enabled = false;
            this.chart1.ChartAreas["ChartArea1"].AxisY.LabelAutoFitStyle = LabelAutoFitStyles.None;
            this.chart1.ChartAreas["ChartArea1"].AxisY.LabelStyle.Font = new Font("Trebuchet MS", 8F);

            this.chart1.GetToolTipText += new EventHandler<ToolTipEventArgs>(this.chart_GetToolTipText);

            this.Controls.Add(this.chart1);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

            endTime = DateTime.Now;
            startTime = endTime.AddHours(-1);
            dtpStartTime.Value = startTime;
            dtpEndTime.Value = endTime;
            bShowLast1Hour = true;
            chkShowLast1HourInfo.Checked = bShowLast1Hour;
        }

        public void AddSeries(List<TLineChartItem> lineChartItemList)
        {
            int index = 0;
            foreach (var item in lineChartItemList)
            {
                if (index == MAX_LINECHART_SERIES_NUM)
                {
                    Console.WriteLine($"LineChart超過最大Series量 : {MAX_LINECHART_SERIES_NUM}");
                    break;
                }                    
                Series series = new Series();
                series.ChartArea = "ChartArea1";
                series.ChartType = SeriesChartType.Line;
                series.Legend = "Legend1";
                series.MarkerBorderWidth = 5;
                series.Name = item.seriesName;
                series.Color = lineChartSeriesColor[index];
                series.BorderWidth = 2;
                this.chart1.Series.Add(series);
                index++;
            }
        }

        public void RefrechChart()
        {
            endTime = bShowLast1Hour ? DateTime.Now : dtpEndTime.Value;
            startTime = bShowLast1Hour ? endTime.AddHours(-1) : dtpStartTime.Value;
            TimeSpan diff = endTime.Subtract(startTime);
            if (diff.Seconds < 0)
            {
                Console.WriteLine("起始時間不可大於結束時間");
                return;
            }

            //防呆設置startTime 不可大於 endTime
            if (SQLFunc != null)
            {
                chartDataTable = SQLFunc.Invoke(startTime, endTime);
                chart1.DataSource = chartDataTable;
                int seriesNum = this.chart1.Series.Count;
                for (int i = 0; i < seriesNum; i++)
                {
                    string seriesName = chart1.Series[i].Name;
                    chart1.Series[seriesName].YValueMembers = seriesName;
                }
                chart1.Refresh();
            }

            Console.WriteLine("Refresh Line Chart");
        }

        private void btnRefreshChart_Click(object sender, EventArgs e)
        {
            this.RefrechChart();                
        }

        /// <summary>
        /// 游標資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chart_GetToolTipText(object sender, ToolTipEventArgs e)
        {
            if (e.HitTestResult.ChartElementType == ChartElementType.DataPoint && this.chartDataTable != null)
            {
                int index = e.HitTestResult.PointIndex;
                DataPoint dp = e.HitTestResult.Series.Points[index];
                string GlassName = this.chartDataTable.Rows[index]["GlassName"].ToString();
                string TactTime = this.chartDataTable.Rows[index]["TactTime"].ToString().Substring(0, 8);
                string SeriesName = e.HitTestResult.Series.Name;
                string info = string.Format($"GlassName : {GlassName}\nTactTime : {TactTime}\n{SeriesName}:{dp.YValues[0]}");

                //鼠標相對於窗體左上角的座標
                Point formPoint = this.PointToClient(Control.MousePosition);
                int x = formPoint.X;
                if (x + pnlHint.Size.Width > this.Width)
                {
                    x = x - pnlHint.Size.Width;
                }
                int y = formPoint.Y;
                //顯示提示信息
                this.pnlHint.Visible = true;
                this.pnlHint.Location = new Point(x, y);
                this.lbHint.Text = info;
            }
            else
            {
                this.pnlHint.Visible = false;
            }
        }

        private void chkShowLast1HourInfo_CheckedChanged(object sender, EventArgs e)
        {
            bShowLast1Hour = chkShowLast1HourInfo.Checked;
        }
    }
}
