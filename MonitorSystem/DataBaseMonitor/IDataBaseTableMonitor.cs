using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorSystem.DataBaseMonitor
{
    public interface IDataBaseTableMonitor
    {
        public void StartMonitor(string Conn);
        public void StopMonitor();
    }
}
