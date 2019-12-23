using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ProGMClient.Business
{
    public class SocketBussiness
    {
        public static TcpClient tcpClient;
        public static void SendData(TcpClient tcpClient, string data)
        {
            if (tcpClient.Connected)
            {
                NetworkStream ns = tcpClient.GetStream();
                byte[] arrByte = Encoding.UTF8.GetBytes(data + "$");
                ns.Write(arrByte, 0, arrByte.Length);
                ns.Flush();
            }
        }
        public static string GetData(TcpClient tcpClient)
        {
            NetworkStream ns = tcpClient.GetStream();
            while (ns.DataAvailable && tcpClient.Connected)
            {
                //ns.Length;
                // byte[] arrByte = new byte[65537];
                byte[] arrByte = new byte[tcpClient.ReceiveBufferSize];
                // byte[] arrByte = new byte[ns.Length];
                ns.Read(arrByte, 0, arrByte.Length);
                string data = Encoding.UTF8.GetString(arrByte);
                data = data.Substring(0, data.LastIndexOf("$"));
                return data.Trim();
            }
            return null;
        }




    }
    public class dataSend
    {
        public string from { get; set; }
        public string msg { get; set; }
        public string name { get; set; }
        public string type { get; set; }

    }
}
