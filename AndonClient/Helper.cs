using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AndonClient
{
    internal class Helper
    {
        public static bool Retry { get; set; }
        public static IPAddress? ComputerIP { get; private set; }
        public static IPAddress? ServerIP { get; set; }
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
        public string CurrentColor { get; private set; }
        public static void SetCurrentColor(string color)
        {
            //color = color.ToLower();
            //switch (color)
            //{
            //    case "green":

            //        break;
            //    case "yellow":

            //        break;
            //    case "red":

            //        break;
            //    default:
            //        break;
            //}

        }

        
    }
}
