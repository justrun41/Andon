using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AndonClient
{
    internal static class UDPSend
    {
        const int SendPort = 11000;
        public static void SendData(IPAddress IP, string DataToSend)
        {
            if (IP != null)
            {
                Socket udpClient = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                IPEndPoint ep = new(IP, SendPort);
                byte[] sendBytes = Encoding.ASCII.GetBytes(Convert.ToBase64String(Encoding.UTF8.GetBytes(DataToSend)));
                if (IP.ToString() == "255.255.255.255")
                {
                    udpClient.EnableBroadcast = true;
                }
                udpClient.SendTo(sendBytes, ep);
                udpClient.Close();
            }
        }
    }

}
