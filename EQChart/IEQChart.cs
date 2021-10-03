using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace EQChart
{
    public struct TBarChartMonitorItem
    {
        public string itemName;
        public int Boundary;
    };

    public struct TLineChartItem
    {
        public string seriesName;
    };

    //2021-09-26 抽象類別
    public abstract class AbstactEQChart
    {
        public abstract void RefreshChart();
    }

    public interface IEQChart
    {
        public void RefrechChart();   //刷新圖表資料內容
    }
}
