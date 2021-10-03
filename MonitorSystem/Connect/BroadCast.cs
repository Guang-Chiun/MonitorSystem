using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace MonitorSystem.Connect
{
    /// <summary>
    /// 廣播功能，向所有client端發送訊息的類別
    /// </summary>
    class BroadCast
    {
        public static void PushMessage(String msg, String uName, Boolean flag, Dictionary<String, Socket> clientList)
        {
            foreach (var item in clientList)
            {
                Socket brdcastSocket = (Socket)item.Value;
                String msgTemp = null;
                Byte[] castBytes = new Byte[4096];
                if (flag == true)
                {
                    msgTemp = uName + ":" + msg + "\t\t" + DateTime.Now.ToString();
                    castBytes = Encoding.UTF8.GetBytes(msgTemp);
                }
                else
                {
                    msgTemp = msg + "\t\t" + DateTime.Now.ToString();
                    castBytes = Encoding.UTF8.GetBytes(msgTemp);
                }
                try
                {
                    brdcastSocket.Send(castBytes);
                }
                catch (Exception ex)
                {
                    brdcastSocket.Close();
                    brdcastSocket = null;
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }
        }
    }
}
