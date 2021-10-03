using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace MonitorSystem.Connect
{
    public class ServerSocket
    {
        #region 公用變數
        public static Dictionary<String, Socket> clientList = null;    //用來保存多個ClientSocket的List
        Socket serverSocket = null;
        Boolean isListen = true;
        Thread thStartListen;    //監聽clientSocket是否連接的執行緒       
        IPAddress _ipAddress;    //監聽IP位址
        int _port;               //綁定的port
        IPEndPoint endPoint;
        #endregion

        #region 建構子
        public ServerSocket() 
        {
            _ipAddress = IPAddress.Loopback;   //預設本地位址127.0.0.1
            _port = 8080;
            InitialParameter();
        }
        public ServerSocket(IPAddress address, int port)
        {
            _ipAddress = address;
            _port = port;
            InitialParameter();
        }
        #endregion

        #region 解構子
        ~ServerSocket()
        {
            StopServerSocket();
        }
        #endregion

        public void StopServerSocket()
        {
            if (serverSocket != null)
            {
                BroadCast.PushMessage("Server has closed", "", false, clientList);
                foreach (var socket in clientList.Values)
                {
                    socket.Close();
                }
                clientList.Clear();
                serverSocket.Close();
                serverSocket = null;
                isListen = false;
                Console.WriteLine("Server Socket服務停止");
            }
        }

        private void InitialParameter()
        {
            try
            {
                clientList = new Dictionary<string, Socket>();
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                endPoint = new IPEndPoint(_ipAddress, _port);
                serverSocket.Bind(endPoint);               //Server Socket綁定端點
                serverSocket.Listen(100);                  //可連接最大clientSocket數量
                thStartListen = new Thread(StartListen);
                thStartListen.IsBackground = true;
                thStartListen.Start();
                Console.WriteLine("Server Socket啟動成功");
            }
            catch (SocketException socketEx)
            {
                Console.WriteLine(socketEx.Message);
            }
        }

        private void StartListen()
        {
            isListen = true;
            Socket clientSocket = default(Socket);
            while (isListen)
            {
                try
                {
                    if (serverSocket == null)
                    {
                        return;
                    }
                    //等待新的clientSocket連接
                    clientSocket = serverSocket.Accept();
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Byte[] bytesFrom = new Byte[4096];
                if (clientSocket != null && clientSocket.Connected)
                {
                    try
                    {
                        Int32 len = clientSocket.Receive(bytesFrom);
                        if (len > -1)
                        {                    
                            String sMsgFromClient = Encoding.UTF8.GetString(bytesFrom, 0, len);                            
                            AnalyseClientMsg(sMsgFromClient, clientSocket);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);                      
                    }
                }
            }
        }

        private void AnalyseClientMsg(string sMsg, Socket clientSocket = null)
        {
            Int32 sublen = sMsg.LastIndexOf("$");
            ESocketSendInfo eSocketSendInfo = (ESocketSendInfo)Convert.ToInt32(sMsg.Substring(0, sublen));
            int length;
            string EQID;
            string sGetInfo = sMsg.Substring(sublen + 1, sMsg.Length - sublen - 1);
            switch (eSocketSendInfo)
            {
                case ESocketSendInfo.CHECK_LOGIC_INFO:
                    length = sGetInfo.LastIndexOf(",");
                    EQID = sGetInfo.Substring(0, length);
                    string Password = sGetInfo.Substring(length + 1, sGetInfo.Length - length - 1); //後面那項是長度
                    if (ConnectConst.AllowLogicEQList.ContainsKey(EQID))
                    {
                        if (ConnectConst.AllowLogicEQList[EQID].Equals(Password))
                        {
                            //允許Logic，可放到clientList中
                            if (!clientList.ContainsKey(EQID))
                            {
                                clientList.Add(EQID, clientSocket);
                                //BroadCast.PushMessage(EQID + "Joined", EQID, false, clientList);
                                HandleClientSocket client = new HandleClientSocket();
                                AnalyseClientMsgDlg dlg = new AnalyseClientMsgDlg(AnalyseClientMsg);    //2021-08-13 Test

                                //2021-08-16 成功上線 (這裡先用程式有成功連線當作Running)
                                TEQRunningStatus tEQRunningStatus;
                                tEQRunningStatus.EQID = EQID;
                                tEQRunningStatus.Running = true;
                                ConnectConst.EQRunningLog.Enqueue(tEQRunningStatus);

                                client.StartClient(clientSocket, EQID, clientList, dlg);
                                Console.WriteLine($"{EQID}已經上線, 時間:{DateTime.Now}");
                            }
                            else
                            {
                                clientSocket.Send(Encoding.UTF8.GetBytes($"#{EQID}重複登錄#"));
                            }
                        }
                        else
                        {
                            clientSocket.Send(Encoding.UTF8.GetBytes("#輸入密碼錯誤#"));
                        }
                    }
                    else
                    {
                        clientSocket.Send(Encoding.UTF8.GetBytes("#該EQID不在監控系統設定表單中#"));
                    }
                    break;

                case ESocketSendInfo.WARNING_INFO:
                    length = sGetInfo.LastIndexOf(",");
                    EQID = sGetInfo.Substring(0, length);
                    string sWarningInfo = sGetInfo.Substring(length + 1, sGetInfo.Length - length - 1);
                    TWarningInfoFromClient warningInfoFromClient;
                    warningInfoFromClient.EQID = EQID;
                    warningInfoFromClient.WarningMessage = sWarningInfo;
                    warningInfoFromClient.errorTime = DateTime.Now;
                    ConnectConst.clientWarningQueue.Enqueue(warningInfoFromClient);
                    break;

                default:
                    break;
            }
        }
    }
}
