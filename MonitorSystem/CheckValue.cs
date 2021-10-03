using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace MonitorSystem
{
    public static class CheckValue
    {
        /// <summary>
        /// 這樣寫就不用一個一個Get Set檢查
        /// 要改成可以檢查類別物件裡面所有資訊，可能每個Property檢查的會不一樣
        /// 檢查數值，目前寫法只能檢查Property
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string GetErrorMsg(object o)   //檢查指定屬性
        {
            string sMsg = String.Empty;
            Type type = o.GetType();
            PropertyInfo[] propertyInfos = type.GetProperties();   //取得類別中的PropertyInfo
            foreach (var property in propertyInfos)
            {
                string propertyName = property.Name;
                PropertyInfo propertyInfo = type.GetProperty(propertyName);
                if (propertyInfo.IsDefined(typeof(AValueCheck), true))   //有寫註釋才會進到這裡
                {
                    object[] oAtt = propertyInfo.GetCustomAttributes(typeof(AValueCheck), true);
                    AValueCheck aValueCheckAttribute = (AValueCheck)oAtt[0];
                    object checkValue = property.GetValue(o);                  
                    if (!aValueCheckAttribute.CheckValue(checkValue))
                    {
                        sMsg = sMsg.Insert(0, aValueCheckAttribute.GetErrorMsg(checkValue) + "\r\n");
                    }                   
                }
            }
            return sMsg;
        }
    }

    /// <summary>
    /// 抽象類別 : 檢查輸入資料抽象類別，可繼承擴充 EX : 檢查double數值範圍、檢查字串、檢查是否在資料庫欄位內
    /// </summary>
    public abstract class AValueCheck : Attribute
    {
        public abstract bool CheckValue(object o);
        public abstract string GetErrorMsg(object o);
    }

    /// <summary>
    /// 檢查輸入值是否在範圍內
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class IntValueCheck : AValueCheck
    {
        private int m_nMin;
        private int m_nMax;
        public IntValueCheck(int nMin, int nMax)
        {
            m_nMin = nMin;
            m_nMax = nMax;
        }
        public override bool CheckValue(object o)
        {
            int fCheckValue = (int)o;
            return (fCheckValue >= m_nMin && fCheckValue <= m_nMax);
        }
        public override string GetErrorMsg(object o)
        {
            string sErrorMsg = String.Empty;
            int fCheckValue = (int)o;
            if (fCheckValue < m_nMin || fCheckValue > m_nMax)
                sErrorMsg = $"Threshold : {fCheckValue}, 不在{m_nMin}~{m_nMax} 範圍內";
            return sErrorMsg;
        }
    }

    /// <summary>
    /// 檢查輸入資料是否包含在資料庫表中的column
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DBColumnCheck : AValueCheck
    {
        //資料表中有那些欄位
        private List<string> m_DBAllColumnList = new List<string>(){};   
        public DBColumnCheck(string TableName)
        {
            Assembly assembly = Assembly.Load("MonitorSystem"); 
            Type type = assembly.GetType($"MonitorSystem.{TableName}");
            PropertyInfo[] propertyInfo = type.GetProperties();
            foreach (var property in propertyInfo)
            {
                string ColumnName = TableColumnName.GetColumnName(property);
                m_DBAllColumnList.Add(ColumnName);
            }
        }
        public override bool CheckValue(object o)
        {
            return m_DBAllColumnList.Contains((string)o);
        }
        public override string GetErrorMsg(object o)
        {
            string sErrorMsg = String.Empty;
            string CheckString = (string)o;
            if (!m_DBAllColumnList.Contains(CheckString))
                sErrorMsg = $"ColumnName : {CheckString}, 不在Glass資料表中範圍內";
            return sErrorMsg;
        }
    }

}
