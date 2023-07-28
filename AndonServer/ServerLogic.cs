using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AndonServer
{
    internal class ServerLogic
    {
        static bool Started = false;
        public static void UDPListener()
        {
            if (!Started) 
            { 
                UDPReceive udpReceive = new();
                Started = true;
            }
            

        }
        
    }
}
