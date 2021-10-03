using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using EQChart;

namespace MonitorSystem.FactoryPattern
{
    /// <summary>
    /// 工廠模式 : 這樣新增擴展時，只要多加一個類別就好，不需要改動到程式碼
    /// </summary>
    public interface IEQChartFactory
    {
        IEQChart CreateEQChart(string ChartEQID, int ID, JsonEQChart jsonEQChart);
    }
}
