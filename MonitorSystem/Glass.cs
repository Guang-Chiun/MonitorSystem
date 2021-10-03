using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace MonitorSystem
{
    /// <summary>
    /// Glass表中項目，並標註於資料庫中的欄位名稱
    /// </summary>
    public class Glass
    {
        [ColumnName("GlassID")]
        public string GlassID { get; set; }
        [ColumnName("Recipe ID")]
        public string Recipe_ID { get; set; }
        [ColumnName("ProcessType")]
        public string ProcessType { get; set; }
        [ColumnName("EQID")]
        public string EQID { get; set; }
        [ColumnName("GlassName")]
        public string GlassName { get; set; }
        [ColumnName("Model Num")]
        public string Model_Num { get; set; }
        [ColumnName("Panel Num")]
        public string Panel_Num { get; set; }
        [ColumnName("DefectNum")]
        public string DefectNum { get; set; }
        [ColumnName("TactTime")]
        public string TactTime { get; set; }
        [ColumnName("NGGlass")]
        public string NGGlass { get; set; }
        [ColumnName("MaskCommonDefect")]
        public string MaskCommonDefect { get; set; }
        [ColumnName("GlassCommonDefect")]
        public string GlassCommonDefect { get; set; }
        [ColumnName("IsFileUpload")]
        public string IsFileUpload { get; set; }
        [ColumnName("S Size Defect Num")]
        public string S_Size_Defect_Num { get; set; }
        [ColumnName("M Size Defect Num")]
        public string M_Size_Defect_Num { get; set; }
        [ColumnName("L Size Defect Num")]
        public string L_Size_Defect_Num { get; set; }
        [ColumnName("O Size Defect Num")]
        public string O_Size_Defect_Num { get; set; }
        [ColumnName("GlassImage")]
        public string GlassImageGlass { get; set; }
        [ColumnName("GlassLevel")]
        public string GlassLevel { get; set; }
    }

    public class ColumnNameAttribute : Attribute
    {
        private string _ColumnName = null;
        public ColumnNameAttribute(string Remark)
        {
            this._ColumnName = Remark;
        }
        public string GetColumnName()
        {
            return this._ColumnName;
        }
    }

    public static class TableColumnName
    {
        public static string GetColumnName(PropertyInfo propertyInfo)
        {
            string ColumnName = String.Empty;
            if (propertyInfo.IsDefined(typeof(ColumnNameAttribute), true))
            {
                object[] o = propertyInfo.GetCustomAttributes(typeof(ColumnNameAttribute), true);
                ColumnNameAttribute columnNameAttribute = (ColumnNameAttribute)o[0];
                ColumnName = columnNameAttribute.GetColumnName();
            }
            return ColumnName;
        }
    }

}
