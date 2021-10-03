using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace EQControl
{
    /// <summary>
    /// 機台警報內容
    /// </summary>
    public class JsonEQWarningInfo
    {
        public string Name { get; set; }     //警報名稱       
        public int ID { get; set; }          //警報ID (參照 WarningInformation 表)
        [DefaultValue(true)]
        public bool Enable { get; set; }     //是否開啟
        [DefaultValue(0)]
        public int Threshold { get; set; }   //臨界值 (有些參數會需要Threshold監控 EX : Defect Overflow)
    }


    /// <summary>
    /// 用來判斷警報之類別
    /// </summary>
    public static class WarningJudge
    {
        private static Dictionary<string, IList<JsonEQWarningInfo>> JsonEQWarningInfoDic = new Dictionary<string, IList<JsonEQWarningInfo>>();

        /// <summary>
        /// key : EQID, Value : 機台對應的警報表
        /// </summary>
        /// <param name="EQID"></param>
        /// <param name="jsonEQWarningInfo"></param>
        public static void AddJsonEQWarningInfoToDic(string EQID, IList<JsonEQWarningInfo> jsonEQWarningInfo)
        {
            if (!JsonEQWarningInfoDic.ContainsKey(EQID))
                JsonEQWarningInfoDic.Add(EQID, jsonEQWarningInfo);
        }

        /// <summary>
        /// 判斷EQ Client傳來的錯誤訊息
        /// </summary>
        /// <param name="sWarningMsg"></param>
        /// <returns></returns>
        public static int JudgeClientEQWarning(ref string sWarningMsg)
        {
            string[] WarningListFromClient = System.Text.RegularExpressions.Regex.Split(sWarningMsg, ";");
            sWarningMsg = "";
            int OverlapEQClientType = 0;
            foreach (string clientWarning in WarningListFromClient)
            {
                if (String.IsNullOrEmpty(clientWarning))
                    continue;
                int Type = Convert.ToInt32(clientWarning);
                if (WarningInformation.m_WarningInfoDic.ContainsKey(Type))
                {
                    OverlapEQClientType |= Type;
                    sWarningMsg += $"{WarningInformation.m_WarningInfoDic[Type]},";
                }
            }
            return OverlapEQClientType;
        }

        public static int JudgeDefectOverflow(string EQID, int DefectNum)
        {
            int Type = 0;
            foreach (var warningInfo in JsonEQWarningInfoDic[EQID])
            {
                if (warningInfo.ID == WarningInformation.DefectOverflow)
                {
                    Console.WriteLine($"警報名稱 : {warningInfo.Name}");
                    bool bEnable = warningInfo.Enable;
                    int Threshold = warningInfo.Threshold;
                    Type |= (bEnable && DefectNum > Threshold) ? WarningInformation.DefectOverflow : 0;
                }
            }
            return Type;
        }

        public static int JudgeMaskCommon(string EQID, bool bMaskCommon)
        {
            int Type = 0;
            foreach (var warningInfo in JsonEQWarningInfoDic[EQID])
            {
                if (warningInfo.ID == WarningInformation.MaskCommon)
                {
                    Console.WriteLine($"警報名稱 : {warningInfo.Name}");
                    bool bEnable = warningInfo.Enable;
                    Type = (bEnable && bMaskCommon) ? WarningInformation.MaskCommon : 0;
                    break;
                }
            }
            return Type;
        }

        public static int JudgeGlassCommon(string EQID, bool bGlassCommon)
        {
            int Type = 0;
            foreach (var warningInfo in JsonEQWarningInfoDic[EQID])
            {
                if (warningInfo.ID == WarningInformation.GlassCommon)
                {
                    Console.WriteLine($"警報名稱 : {warningInfo.Name}");
                    bool bEnable = warningInfo.Enable;
                    Type = (bEnable && bGlassCommon) ? WarningInformation.GlassCommon : 0;
                    break;
                }
            }
            return Type;
        }
    }
}
