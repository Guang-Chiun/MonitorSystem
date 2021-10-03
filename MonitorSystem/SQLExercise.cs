using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MonitorSystem
{
    public static class SQLExercise
    {
        static SqlConnection sqlConnection;

        /// <summary>
        /// DataReader、Command使用
        /// sCommand : SQL指令或是預存程序
        /// 可一行一行讀取選取到的資料
        /// </summary>
        public static void SqlDataReader(string sCommand)
        {
            //SqlCommand sqlcommand = new SqlCommand(sCommand, sqlConnection);                  
            SqlCommand sqlcommand = new SqlCommand(sCommand);
            SqlDataReader dr;
            dr = sqlcommand.ExecuteReader();

            //是否有辦法取得欄位名稱
            var columnInfos = new List<string>();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                string sColumnName = dr.GetName(i);
                columnInfos.Add(sColumnName);
            }
        
            for (int i = 0; i < columnInfos.Count; i++)
            {
                string columnName = columnInfos[i];
                //BOF、EOF使用 : 讀取指定欄位名稱的所有資料
                string readinfo = "";            
                while (dr.Read())   //判斷是否讀到了EOF
                {
                    int a = dr.GetInt32(0);   //可以用各種get取得不同資料型態資料(效率會比較好)  
                    readinfo = dr[columnName].ToString();
                }
                //dr.Close();
                //dr = sqlcommand.ExecuteReader();
            }                             
        }

        /// <summary>
        /// DataSet為客戶端中的記憶體，可暫時存放從Server端取得的資料，就算斷線，還是可以取得暫存資料
        /// 其中包括
        /// DataTable : 存取資料表用
        /// DataAdapter : 傳送指令用的橋樑
        /// 觀念:DataSet中可以包含多個資料表
        /// </summary>
        public static void SQLDataSet(string sCommand1, string sCommand2)
        {
            DataSet ds = new DataSet();
            
            //第一個資料表
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sCommand1, sqlConnection);
            string sTempTableName = "Test";
            sqlDataAdapter.Fill(ds, sTempTableName);
            DataTable dataTable = ds.Tables[sTempTableName];
            string sName = dataTable.TableName;

            //第二個資料表
            SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter(sCommand2, sqlConnection);
            sqlDataAdapter2.Fill(ds, "GlassInfo");
            DataTable table2 = ds.Tables[1];
            int tableNum = ds.Tables.Count;
        }

        /// <summary>
        /// 得到並返回DataTable物件
        /// 以及取得DataTable物件中的資訊
        /// </summary>
        /// <param name="sCommand"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sCommand)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sCommand, sqlConnection);
            string sTempTableName = "Test";
            sqlDataAdapter.Fill(ds, sTempTableName);

            DataTable dataTable = ds.Tables[sTempTableName];

            //由dataTable中取得Row, Column資料
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                StringBuilder sInfo = new StringBuilder();
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    string columnName = dataTable.Columns[j].ColumnName;
                    sInfo.Append($"{columnName} : {dataTable.Rows[i][columnName]}, ");
                }
                Console.WriteLine(sInfo);              
            }
            return dataTable;
        }

        /// <summary>
        /// 返回DataSet物件
        /// </summary>
        /// <param name="sCommand"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sCommand)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sCommand, sqlConnection);
            string sTempTableName = "Test";
            sqlDataAdapter.Fill(ds, sTempTableName);
            return ds;
        }
    }
}
