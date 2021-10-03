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
    /// 創建資料庫連線物件的介面
    /// </summary>
    public interface IDataBaseFactory
    {
        DbConnection CreateDataBaseConnect(string Conn);   
    }
 
}
