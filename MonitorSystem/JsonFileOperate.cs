using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel;
using EQControl;
using EQChart;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace MonitorSystem
{
    /// <summary>
    /// 機台Json檔內容
    /// </summary>
    public class JsonEQInformation
    {        
        public string EQID { get; set; }                                 //機台ID
        public string EQPassword { get; set; }                           //機台登錄監控系統密碼
        public string EQName { get; set; }                               //機台名稱
        public Point EQPosition { get; set; }                            //機台於UI上的位置 (可擴充為產線中位置進行座標轉換至UI)
        public Size EQSize { get; set; }                                 //機台於UI上的Size (可擴充為產線中機台平面長寬，縮放為UI長寬)
        public IList<JsonEQWarningInfo> EQWarningInfoList { get; set; }  //該機台需要監控的警報表
        public IList<JsonEQChart> EQChartList { get; set; }              //該機台需要顯示的chart圖表
    }
    
    /// <summary>
    /// 每台機檯需要顯示的圖表
    /// </summary>
    public class JsonEQChart
    {
        public string chartType { get; set; }                                //建立圖表類型
        public string ShowChartName { get; set; }                            //圖表顯示於UI上的名稱
        public IList<JsonEQChartMonitorItem> MonitorItemList { get; set; }   //圖表內需要監控的資料庫項目集
    }

    /// <summary>
    /// 圖表內容需要監控的詳細項目
    /// </summary>
    public class JsonEQChartMonitorItem
    {
        [DBColumnCheck("Glass")]                 //檢查Glass表格
        public string ColumnName { get; set; }   //資料庫表中的columnName
        [DefaultValue(0)]                        //預設為0
        [IntValueCheck(0, 2000)]                 //設定範圍要在0~2000
        public int Threshold { get; set; }       //臨界值 (有些參數會需要Threshold監控 EX : Defect Num，若不需要預設0)
    }

    public static class JsonFileOperate
    {
        /// <summary>
        /// 存放EQ Json檔資料夾位置
        /// </summary>
        public static string JsonEQFileDir = System.Environment.CurrentDirectory + "\\EQJsonFile\\";   //"D:\\C#\\MonitorSystem\\MonitorSystem\\EQJsonFile\\";                             

        /// <summary>
        /// 存放所有JSON檔案資訊的陣列
        /// </summary>
        public static List<JsonEQInformation> JsonEQInformationList = new List<JsonEQInformation>();

        /// <summary>
        /// 處理中文編碼問題
        /// </summary>
        private static JsonSerializerOptions options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true
        };

        /// <summary>
        /// 序列化JsonEQInformation class
        /// </summary>
        public static void Serialize()
        {
            var JsonEQInformation = new JsonEQInformation
            {
                EQID = "EQ1",
                EQName = "AOI1A",
                EQPassword = "123456",
                EQPosition = new Point(335, 37),
                EQSize = new Size(148, 101),
                EQWarningInfoList = new List<JsonEQWarningInfo>()
                {
                    //Warning 1
                    new JsonEQWarningInfo
                    {
                        Name = "Defect Overflow",
                        ID = WarningInformation.DefectOverflow,
                        Enable = true,
                        Threshold = 500
                    },
                    //Warning 2
                    new JsonEQWarningInfo
                    {
                        Name = "Mask Common",
                        ID = WarningInformation.MaskCommon,
                        Enable = true,
                    },
                    //Warning 3
                    new JsonEQWarningInfo
                    {
                        Name = "Glass Common",
                        ID = WarningInformation.GlassCommon,
                        Enable = true,
                    }
                },

                EQChartList = new List<JsonEQChart>()
                {
                    //第一張Chart圖
                    new JsonEQChart
                    {                     
                        chartType = nameof(BarChart),
                        ShowChartName = "測試最近30張Glass缺陷資訊直方表",
                        MonitorItemList = new List<JsonEQChartMonitorItem>()
                        {
                            new JsonEQChartMonitorItem
                            {
                                ColumnName = "DefectNum",
                                Threshold = 450
                            },

                            new JsonEQChartMonitorItem
                            {
                                ColumnName = "MaskCommonDefect"
                            },
                        },
                    },

                    //第二張Chart圖
                    new JsonEQChart
                    {
                        chartType = nameof(LineChart),
                        ShowChartName = "測試時間缺陷數量圖",
                        MonitorItemList = new List<JsonEQChartMonitorItem>()
                        {
                            new JsonEQChartMonitorItem { ColumnName = "DefectNum"},
                            new JsonEQChartMonitorItem { ColumnName = "M Size Defect Num"},
                            new JsonEQChartMonitorItem { ColumnName = "L Size Defect Num"},
                            new JsonEQChartMonitorItem { ColumnName = "O Size Defect Num"},
                        },
                    },
                }
            };

            string fileName = "EQ1.json";
            string jsonString = JsonSerializer.Serialize(JsonEQInformation, options);
            File.WriteAllText(JsonEQFileDir + fileName, jsonString, Encoding.UTF8);
        }

        /// <summary>
        /// 讀取機台資訊json檔案
        /// </summary>
        public static void ReadJsonEQInformation()
        {
            try
            {
                JsonEQInformationList.Clear();
                foreach (string filePath in System.IO.Directory.GetFileSystemEntries(JsonEQFileDir, "*.json"))
                {
                    string jsonString = File.ReadAllText(filePath, Encoding.UTF8);                
                    JsonEQInformation jsonEQInformation = JsonSerializer.Deserialize<JsonEQInformation>(jsonString, options);   //反序列化讀取資料                  
                    JsonEQInformationList.Add(jsonEQInformation);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }          
        }
    }
}
