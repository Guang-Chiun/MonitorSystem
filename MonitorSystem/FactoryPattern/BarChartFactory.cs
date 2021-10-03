using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Data;
using MonitorSystem.DataBase;
using EQChart;

namespace MonitorSystem.FactoryPattern
{
    /// <summary>
    /// 創建長條圖的類別
    /// </summary>
    public class BarChartFactory : IEQChartFactory
    {
        public IEQChart CreateEQChart(string ChartEQID, int ID, JsonEQChart jsonEQChart)
        {
            BarChart barChart = new BarChart();
            barChart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            barChart.Location = new Point(0, 30);
            barChart.Name = $"{ChartEQID}{jsonEQChart.chartType}{ID}";
            barChart.Size = new System.Drawing.Size(490, 250);
            barChart.Visible = false;

            string columnEQID = ChartEQID.Substring(2, ChartEQID.Length - 2);
            
            // FUNC 委託
            Func<string, DataTable> SelectInfoFunc = ColumnInfo =>
            {
                //string selectCommandText = $"select top 30 GlassName, {"[" + ColumnInfo + "]"} from Glass where TactTime <= '{DateTime.Now.ToString("HH:mm:ss")}' and EQID = {columnEQID} order by TactTime desc";
                //string selectConnectionString = StaticSQLSerCommond.GetConnectionInformation();
                //IDataBaseCommand dataBaseCommand = new SQLServerCommand();
                //return dataBaseCommand.GetDataTable(selectCommandText, selectConnectionString);
                return StaticSQLServerCommond.CallProc_GetBarChartInfo(ColumnInfo, columnEQID);                
            };
            barChart.SQLFunc = SelectInfoFunc;

            //加入監控項目
            List<TBarChartMonitorItem> MonitorList = new List<TBarChartMonitorItem>();
            foreach (var item in jsonEQChart.MonitorItemList)
            {
                TBarChartMonitorItem barChartMonitorItem;
                barChartMonitorItem.itemName = item.ColumnName;
                barChartMonitorItem.Boundary = item.Threshold;
                MonitorList.Add(barChartMonitorItem);
            }
          
            barChart.SetMonitorItem(MonitorList);
            
            return barChart;
        }
    }
}
