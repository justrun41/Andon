using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Linq;

namespace AndonClient
{
    internal  class ClientLogic
    {
        private static string? ComputerName { get; set; }
        public static string CurrentColor { get; set; }
        private static string? ServerName { get; set; }

        public static void Initalize()
        {
            try
            {
                ComputerName = Dns.GetHostName();
                XmlDocument xmlServerConn = new();
                xmlServerConn.Load("ServerConnection.xml");
                XmlNodeList nodes = xmlServerConn.DocumentElement.SelectNodes("/AndonServer");
                foreach (XmlNode node in nodes)
                {
                    ServerName = node.SelectSingleNode("ComputerName").InnerText;
                }
                TCPSend.Connect(ServerName, $"ComputerName:{ComputerName},ColorCode:WhatsMyColor"); 
            }
            catch (Exception)
            {
                throw;
            }
        }
        //private static void GetCurrentColor()
        //{
        //    TCPSend.Connect(ServerName, $"ComputerName:{ComputerName},WhatsMyColor");
        //}
        public static void ButtonLogic(Button b)
        {
            switch (b.Background.ToString())
            {
                case "#FF008000":  //Green
                     TCPSend.Connect(ServerName, $"ComputerName:{ComputerName},ColorCode:Green");
                    break;
                case "#FFFFFF00": //Yellow
                    TCPSend.Connect(ServerName, $"ComputerName:{ComputerName},ColorCode:Yellow");
                    break;
                case "#FFFF0000": //Red
                    TCPSend.Connect(ServerName, $"ComputerName:{ComputerName},ColorCode:Red");
                    break;
                default:
                    break;
            }
        }
        ////Buttons_Panel.Background = new SolidColorBrush(Colors.Red);
    }
}
