using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AndonClient
{
    internal static class ClientLogic
    {
        
        public static void Initalize()
        {
            UDPReceive.AsyncAwaitUDP();
            FindServer();
        }
        private static void FindServer()
        {
            Helper.GetIPAddress();
            UDPSend.SendData(IPAddress.Broadcast,$"Client,{Helper.ComputerIP}");
            Debug.WriteLine(Helper.ComputerIP);
        }

        public static void ButtonLogic(Button b)
        {
            switch (b.Background.ToString())
            {
                case "#FF008000":
                    Debug.Write("Green");
                    UDPSend.SendData(Helper.ServerIP,"Green");
                    break;
                case "#FFFFFF00":
                    Debug.Write("Yellow");
                    UDPSend.SendData(Helper.ServerIP, "Yellow");
                    break;
                case "#FFFF0000":
                    Debug.Write("Red");
                    UDPSend.SendData(Helper.ServerIP, "Red");
                    break;
                default:
                    break;
            }
        }

        ////Buttons_Panel.Background = new SolidColorBrush(Colors.Red);
    }
}
