using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;
using TableDependency.SqlClient.Base.Enums;

namespace MonitorSystem.DataBaseMonitor
{
    public class MonitorSQLServerGlassTable : IDataBaseTableMonitor
    {
        private SqlTableDependency<Glass> tbDep;    
        public MonitorSQLServerGlassTable() 
        {            
        }      
        public void StartMonitor(string Conn)
        {
            try
            {
                tbDep = new SqlTableDependency<Glass>(Conn, executeUserPermissionCheck : false);
                tbDep.OnChanged += Dep_Changed;
                tbDep.OnError += TbDep_OnError;
                tbDep.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }          
        }

        public void StopMonitor()
        {
            tbDep.Stop();
        }
        private void Dep_Changed(object sender, RecordChangedEventArgs<Glass> e)
        {
            //這樣寫才可以監控表格內詳細資訊
            //預計之後將變動資訊放到一個Queue裡面
            switch (e.ChangeType)
            {
                case ChangeType.Insert:
                    FmMain.DataBaseUpdateInfoQueue.Enqueue(e.Entity);
                    break;
            }
        }

        private void TbDep_OnError(object sender, ErrorEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
