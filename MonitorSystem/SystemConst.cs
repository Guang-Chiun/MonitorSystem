using System;
using System.Collections.Generic;
using System.Text;
using EQChart;
using System.Windows.Forms;

namespace MonitorSystem
{
    //2021-09-26 存放某個EQID有哪些control物件
    public struct TEQChartInfo
    {
        public string chartName;
        public IEQChart EQChart;
        public Control ChartUI;
    }

    class SystemConst
    {
    }
}
