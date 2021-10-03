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
    public class HandleClientSocket
    {
        Socket clientSocket;
        String clNo;
        Dictionary<String, Socket> clientList = new Dictionary<string, Socket>();
        AnalyseClientMsgDlg AnalyseClientMsgDlg;
        public HandleClientSocket() { }
        object LockTest = new object();

        public void StartClient(Socket inClientSocket, String clientNo, Dictionary<String, Socket> cList, AnalyseClientMsgDlg analyseClientMsgDlg)
        {
            clientSocket = inClientSocket;
            clNo = clientNo;
            clientList = cList;
            AnalyseClientMsgDlg = analyseClientMsgDlg;
            Thread thread = new Thread(ReceiveWarning);
            thread.Priority = ThreadPriority.Lowest;
            thread.IsBackground = false;
            thread.Start();
        }

        private void ReceiveWarning()
        {
            Console.WriteLine($"CREATE ID : {Thread.CurrentThread.ManagedThreadId}");
            Byte[] bytesFromClient = new Byte[4096];
            String dataFromClient;
            Byte[] bytesSend = new Byte[4096];
            Boolean isListen = true;

            while (isListen)
            {
                Thread.Sleep(1);   //加入sleep，否則迴圈可能導致CPU飆高
                try
                {
                    if (clientSocket == null || !clientSocket.Connected)
                    {
                        return;
                    }

                    if (clientSocket.Available > 0)
                    {
                        Int32 len = clientSocket.Receive(bytesFromClient);
                        Console.WriteLine($"client socket len : {len}");   //有發訊息才會進到這
                        if (len > -1)
                        {
                            dataFromClient = Encoding.UTF8.GetString(bytesFromClient, 0, len);
                            //2021-08-12 若是斷線會進到這裡，表示收到client傳送的$
                            if (!String.IsNullOrWhiteSpace(dataFromClient))
                            {
                                if (String.IsNullOrWhiteSpace(dataFromClient.Substring(0, dataFromClient.LastIndexOf("$"))))
                                {
                                    isListen = false;

                                    lock (LockTest)
                                    {
                                        clientList.Remove(clNo);
                                        //2021-08-16 Client端中斷連線
                                        TEQRunningStatus tEQRunningStatus;
                                        tEQRunningStatus.EQID = clNo;
                                        tEQRunningStatus.Running = false;
                                        ConnectConst.EQRunningLog.Enqueue(tEQRunningStatus);
                                        Console.WriteLine($"{clNo}已經與Server端斷開連結, {DateTime.Now}");
                                    }

                                    clientSocket.Close();
                                    clientSocket = null;
                                }
                                else
                                {
                                    AnalyseClientMsgDlg.Invoke(dataFromClient);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //2021-08-16 Client端中斷連線
                    isListen = false;

                    lock (LockTest)
                    {
                        clientList.Remove(clNo);
                        //2021-08-16 Client端中斷連線
                        TEQRunningStatus tEQRunningStatus;
                        tEQRunningStatus.EQID = clNo;
                        tEQRunningStatus.Running = false;
                        ConnectConst.EQRunningLog.Enqueue(tEQRunningStatus);
                        Console.WriteLine($"{clNo}已經與Server端斷開連結, {DateTime.Now}");
                    }

                    clientSocket.Close();
                    clientSocket = null;
                }

            }

        }
    }
}
