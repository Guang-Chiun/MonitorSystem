using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Data;
using EQChart;
using MonitorSystem.DataBase;

namespace MonitorSystem.FactoryPattern
{
    /// <summary>
    /// 創建折線圖的類別
    /// </summary>
    public class LineChartFactory : IEQChartFactory
    {
        public IEQChart CreateEQChart(string ChartEQID, int ID, JsonEQChart jsonEQChart)
        {
            LineChart lineChart = new LineChart();
            lineChart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            lineChart.Location = new Point(0, 30);
            lineChart.Name = $"{ChartEQID}{jsonEQChart.chartType}{ID}";
            lineChart.Size = new System.Drawing.Size(490, 250);
            lineChart.Visible = false;
            string columnEQID = ChartEQID.Substring(2, ChartEQID.Length - 2);

            Func<DateTime, DateTime, DataTable> SelectInfoFunc = (startTime, endTime) =>
            {
                //搜尋字串另外寫到別的類別，或是改成預存函數
                //string selectCommandText =
                //$"select * from Glass where TactTime >= '{startTime.ToString("HH:mm:ss")}' and TactTime <= '{endTime.ToString("HH:mm:ss")}' and EQID = {columnEQID} order by TactTime";
                //string selectConnectionString = StaticSQLSerCommond.GetConnectionInformation();
                //IDataBaseCommand dataBaseCommand = new SQLServerCommand();    //一樣可改成reflection
                //return dataBaseCommand.GetDataTable(selectCommandText, selectConnectionString);
                return StaticSQLServerCommond.CallProc_GetLineChartInfo(startTime, endTime, Convert.ToInt32(columnEQID));
            };
            lineChart.SQLFunc = SelectInfoFunc;

            //監控項目
            List<TLineChartItem> lineChartItems = new List<TLineChartItem>();
            foreach (var item in jsonEQChart.MonitorItemList)
            {
                TLineChartItem lineChartItem;
                lineChartItem.seriesName = item.ColumnName;
                lineChartItems.Add(lineChartItem);
            }                    
            lineChart.AddSeries(lineChartItems);
            
            return lineChart;
        }
    }
}
