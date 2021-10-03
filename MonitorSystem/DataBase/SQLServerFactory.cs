using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.SqlClient;

namespace MonitorSystem.DataBase
{
    /// <summary>
    /// 創建SQL Server連線物件，因為可能會斷線、切換使用這之類的，所以不適合用單例
    /// </summary>
    public class SQLServerFactory : IDataBaseFactory
    {
        private SqlConnection _sqlConnection;
        public DbConnection CreateDataBaseConnect(string Conn)
        {
            _sqlConnection = new SqlConnection(Conn);
            Console.WriteLine("建立SQL Server Connection物件");
            return _sqlConnection;
        }
    }
}
