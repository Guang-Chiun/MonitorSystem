using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace EQControl
{
    /// <summary>
    /// 紅燈: 嚴重故障(停機報警). 黃色: 一般故障(無須停機)
    /// </summary>
    public enum EWarningSeverity { OK = 0, YELLOW = 1, RED = 2 }

    /// <summary>
    /// 記錄警報資訊
    /// </summary>
    public static class WarningInformation
    {
        //使用1,2,4....可使用二進位交集聯集概念知道有哪些警報類型
        public const int DefectOverflow = 1;
        public const int MaskCommon     = 2;
        public const int GlassCommon    = 4;
        public const int CCDError       = 8;
        public const int LoadError      = 16;
        public const int UnloadError    = 32;
        public const int SoftError      = 64;

        /// <summary>
        /// 記錄各種警報類型要回報顯示的資訊
        /// </summary>
        public static Dictionary<int, string> m_WarningInfoDic = new Dictionary<int, string>()
        {
            {DefectOverflow, "缺陷過多" },
            {MaskCommon, "Mask Common缺陷" },
            {GlassCommon, "Glass Common缺陷" },
            {CCDError, "CCD異常" },
            {LoadError, "進片異常" },
            {UnloadError, "出片異常" },
            {SoftError, "軟體異常" },
        };

        /// <summary>
        /// 記錄每種警報類型的嚴重性
        /// </summary>
        public static Dictionary<int, EWarningSeverity> m_WarningSeverityDic = new Dictionary<int, EWarningSeverity>()
        {
            {DefectOverflow, EWarningSeverity.YELLOW },
            {MaskCommon, EWarningSeverity.YELLOW },
            {GlassCommon, EWarningSeverity.YELLOW },
            {CCDError, EWarningSeverity.RED },
            {LoadError, EWarningSeverity.YELLOW },
            {UnloadError, EWarningSeverity.YELLOW },
            {SoftError, EWarningSeverity.RED },
        };

    }
}
