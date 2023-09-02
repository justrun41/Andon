using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AndonServer
{
    internal static class Helper
    {
        public static IPAddress? ComputerIP  { get; private set; }

        public static string ConnectionString { get; set; }
        public static void GetIPAddress()
        {
            try
            {
                if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                    using Socket socket = new(AddressFamily.InterNetwork, SocketType.Dgram, 0);
                    socket.Connect("8.8.8.8", 65530);
                    IPEndPoint? endPoint = socket.LocalEndPoint as IPEndPoint;
                    ComputerIP = endPoint.Address;
                }
                else
                {
                    ComputerIP = IPAddress.Parse("127.0.0.1");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
