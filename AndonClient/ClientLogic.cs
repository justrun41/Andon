using AndonServer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AndonClient
{
    internal static class ClientLogic
    {
        public static void Initalize()
        {
            FindServer();
        }
        private static void FindServer()
        {
            Helper.GetIPAddress();
            UDPSend.SendData(IPAddress.Broadcast,$"Hello,{Helper.ComputerIP}");
            Debug.WriteLine(Helper.ComputerIP);
        }
    }
}
