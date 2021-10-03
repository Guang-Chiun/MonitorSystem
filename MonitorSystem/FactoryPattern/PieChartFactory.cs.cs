using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Data;
using EQChart;
using MonitorSystem.DataBase;

namespace MonitorSystem.FactoryPattern
{
    public class PieChartFactory : IEQChartFactory
    {
        public IEQChart CreateEQChart(string ChartEQID, int ID, JsonEQChart jsonEQChart)
        {
            PieChart pieChart = new PieChart();
            pieChart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pieChart.Location = new Point(0, 30);
            pieChart.Name = $"{ChartEQID}{jsonEQChart.chartType}{ID}";
            pieChart.Size = new System.Drawing.Size(490, 250);
            pieChart.Visible = false;
            int columnEQID = Convert.ToInt32(ChartEQID.Substring(2, ChartEQID.Length - 2));
            Func<DataTable> SelectInfoFunc = () => {    return StaticSQLServerCommond.CallProc_GetPieChartProcessInfo(columnEQID);  };
            pieChart.SQLFunc = SelectInfoFunc;
            return pieChart;
        }
    }
}
