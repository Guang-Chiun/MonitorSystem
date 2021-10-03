using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace MonitorSystem.DataBase
{
    public static class StaticSQLServerCommond
    {
        static DataSet ds = new DataSet();

        /// <summary>
        /// 取得SqlConnection連線所需資訊
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionInformation()
        {
            string sServerName = "localhost\\SQLEXPRESS";   //LAPTOP-FEM2RE78\\SQLEXPRESS   localhost
            string sDataBase = "AOIDB";
            string sID = "sa";
            string sPassword = "123456789";
            int TimeOut = 10;
            string connString = $"server={sServerName};database={sDataBase};uid={sID};pwd={sPassword};connect timeout={TimeOut}";
            return connString;
        }

        /// <summary>
        /// 調用取得BarChart資訊之預存函數
        /// </summary>
        /// <param name="sColumnName">
        /// 查詢欄位名稱
        /// </param>
        /// <param name="sEQID">
        /// 機台ID
        /// </param>
        /// <returns></returns>
        public static DataTable CallProc_GetBarChartInfo(string sColumnName, string sEQID)
        {
            using (SqlConnection sqlconnection = new SqlConnection(GetConnectionInformation()))
            {
                sqlconnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlconnection;
                string StoredProcedureName = "proc_GetBarChartInfo";
                sqlCommand.CommandText = StoredProcedureName;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@COLUMN_INFO", SqlDbType.VarChar));
                sqlCommand.Parameters["@COLUMN_INFO"].Value = sColumnName;
                sqlCommand.Parameters.Add(new SqlParameter("@EQID", SqlDbType.VarChar));
                sqlCommand.Parameters["@EQID"].Value = sEQID;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = sqlCommand;
                ds.Tables.Clear();
                sqlDataAdapter.Fill(ds);
                DataTable table = ds.Tables[0];
                return table;
            }
        }

        /// <summary>
        /// 取得LineChart資訊之預存函數
        /// </summary>
        /// <param name="startTime">
        /// 起始時間
        /// </param>
        /// <param name="endTime">
        /// 結束時間
        /// </param>
        /// <param name="EQID"></param>
        /// <returns></returns>
        public static DataTable CallProc_GetLineChartInfo(DateTime startTime, DateTime endTime, int EQID)
        {
            using (SqlConnection sqlconnection = new SqlConnection(GetConnectionInformation()))
            {
                sqlconnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlconnection;
                string StoredProcedureName = "proc_GetLineChartInfo";
                sqlCommand.CommandText = StoredProcedureName;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@START_TIME", SqlDbType.Time));
                sqlCommand.Parameters["@START_TIME"].Value = startTime.TimeOfDay;
                sqlCommand.Parameters.Add(new SqlParameter("@END_TIME", SqlDbType.Time));
                sqlCommand.Parameters["@END_TIME"].Value = endTime.TimeOfDay;
                sqlCommand.Parameters.Add(new SqlParameter("@EQID", SqlDbType.Int));
                sqlCommand.Parameters["@EQID"].Value = EQID;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = sqlCommand;
                ds.Tables.Clear();
                sqlDataAdapter.Fill(ds);
                DataTable table = ds.Tables[0];
                return table;
            }
        }

        /// <summary>
        /// 取得PieChart生產資訊之預存函數
        /// </summary>
        /// <param name="EQID"></param>
        /// <returns></returns>
        public static DataTable CallProc_GetPieChartProcessInfo(int EQID)
        {
            using (SqlConnection sqlconnection = new SqlConnection(GetConnectionInformation()))
            {
                sqlconnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlconnection;
                string StoredProcedureName = "proc_GetPieChartProcessInfo";
                sqlCommand.CommandText = StoredProcedureName;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@EQID", SqlDbType.Int));
                sqlCommand.Parameters["@EQID"].Value = EQID;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = sqlCommand;
                ds.Tables.Clear();
                sqlDataAdapter.Fill(ds);
                DataTable table = ds.Tables[0];
                return table;
            }
        }

    }
}