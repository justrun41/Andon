using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;

namespace AndonServer
{
    internal static class ServerLogic
    {
        private static bool Started = false;
        public static void Listener()
        {
            if (!Started) 
            {
                Helper.GetIPAddress();
                TCPReceiver.BeginListen();
                Started = true;
            }
        }
    }
}
