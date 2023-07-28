using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AndonServer
{
    internal static class UDPSend
    {
        //IPAddress _ip;
        //private string _datatosend;
        const int sendPort = 7676;
        //public UDPSend(IPAddress ip, string datatosend)
        //{
        //    _ip = ip;
        //    _datatosend = datatosend;
        //}
        public static void SendData()
        {
            //if (_ip != null)
            //{
                Socket udpClient = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                IPEndPoint ep = new(IPAddress.Parse("192.0.0.1"), sendPort);

                byte[] sendBytes = Encoding.ASCII.GetBytes(Convert.ToBase64String(Encoding.UTF8.GetBytes("turd,fart")));

                //foreach (byte b in sendBytes)
                //{
                //    Console.Write(Convert.ToChar( b));
                //}
                //Console.WriteLine("");
                //string encryptedDatatoSend = Base64Encode(_datatosend);
                //byte[] sendBytes = Encoding.ASCII.GetBytes(encryptedDatatoSend);
                udpClient.EnableBroadcast = true;
                udpClient.SendTo(sendBytes, ep);
                udpClient.Close();
           // }
        }

        //public static void SendData(IPAddress ip, string datatosend)
        //{
        //    _ip = ip;
        //    _datatosend = datatosend;
        //    SendData();
        //}
    }
}
