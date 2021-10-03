using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace MonitorSystem.Connect
{
    /// <summary>
    /// 寫一個列舉記錄要傳送的資訊類別
    /// </summary>
    public enum ESocketSendInfo
    {
        CHECK_LOGIC_INFO = 1,
        WARNING_INFO = 2
    }

    public struct TEQRunningStatus
    {
        public string EQID;
        public bool Running;
    }

    public struct TWarningInfoFromClient
    {
        public string EQID;
        public string WarningMessage;
        public DateTime errorTime;
    }

    public delegate void AnalyseClientMsgDlg(string sMsg, Socket clientSocket = null);

    public static class ConnectConst
    {
        //用來存上線以及斷線狀態
        public static Queue<TEQRunningStatus> EQRunningLog = new Queue<TEQRunningStatus>();
        //用來放client傳送來的警報訊息
        public static Queue<TWarningInfoFromClient> clientWarningQueue = new Queue<TWarningInfoFromClient>();

        /// <summary>
        /// 寫一個Dictionary儲存可登錄的帳密, 後續要改成由記事本讀入資料(Json??)
        /// </summary>
        public static Dictionary<string, string> AllowLogicEQList = new Dictionary<string, string>();
    }

}
