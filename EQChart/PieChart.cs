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
    public partial class PieChart : UserControl, IEQChart
    {
        public Func<DataTable> SQLFunc = null;
        private DataTable chartDataTable = null;

        private Chart chart1 = new Chart();
        private ChartArea chartArea1 = new ChartArea();
        private Legend legend1 = new Legend();
        Color[] _colors = new Color[] { Color.Lime, Color.Yellow, Color.Red };
        String[] _legendMarkInfo = new String[] { "OK貨", "待修補貨", "NG貨" };

        public PieChart()
        {
            InitializeComponent();
            InitialChart();
        }

        private void InitialChart()
        {
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Alignment = System.Drawing.StringAlignment.Center;
            legend1.AutoFitMinFontSize = 5;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.LegendItemOrder = System.Windows.Forms.DataVisualization.Charting.LegendItemOrder.ReversedSeriesOrder;
            legend1.Name = "Legend1";
            legend1.TitleForeColor = System.Drawing.Color.Maroon;
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this.chart1.Size = new Size(this.Width, this.Height - 25);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";

            Series _series = new Series();
            _series["PieLabelStyle"] = "Outside";   //將文字移到外側 
            _series["PieLineColor"] = "Black";      //繪製黑色的連線
            _series.ChartType = SeriesChartType.Pie;
            //_series.IsValueShownAsLabel = true;
            chart1.Series.Add(_series);

            this.Controls.Add(this.chart1);
        }

        public void RefrechChart()
        {
            if (SQLFunc != null)
            {
                //取得值
                chartDataTable = SQLFunc.Invoke();
                if (chartDataTable.Rows.Count == 0)
                    return;
                Series _series = this.chart1.Series[0];
                int nOK = (int)chartDataTable.Rows[0].ItemArray[0];
                int nRepair = (int)chartDataTable.Rows[0].ItemArray[1];
                int nNG = (int)chartDataTable.Rows[0].ItemArray[2];
                List<int> getValue = new List<int>() { nOK, nRepair, nNG };
                _series.Points.DataBindXY(_legendMarkInfo, getValue);
                int index = 0;
                int Sum = (int)getValue.Sum();
                if (Sum <= 0)
                    return;

                foreach (var points in _series.Points)
                {
                    points.Color = _colors[index];
                    int value = (int)getValue[index];
                    string Percentage = ((float)value / (float)Sum * 100).ToString("f2");
                    string showLabel = $"{ Percentage }%";
                    points.Label = showLabel;
                    points.LegendText = _legendMarkInfo[index];
                    index += 1;
                }
                lbTotalNum.Text = $"本日生產數量 : {Sum}, {_legendMarkInfo[0]} : {nOK}, {_legendMarkInfo[1]} : {nRepair}, {_legendMarkInfo[2]} : {nNG}";
                Console.WriteLine("Refresh PieChart");
            }
            
        }
    }
}
