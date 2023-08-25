using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace AndonServer
{
    internal static class ServerLogic
    {
        private static bool Started = false;
        private static List<Models.ClientData>? ClientDataList { get; set; }
        public static void Listener()
        {
            if (!Started) 
            {
                Helper.GetIPAddress();
                ClientDataList = new();
                TCPReceiver.BeginListen();
                Started = true;
            }
        }
        public static string ClientDataSorter(string data)
        {
            string[] s = data.Split(',');
            string _ComputerName = s[0].Remove(0, s[0].IndexOf(':') + 1);
            string _ColorCode = s[1].Remove(0, s[1].IndexOf(':') + 1);
            string? reply = null;
            Models.ClientData found = ClientDataList.Find(x => x.ComputerName == _ComputerName);
            switch (_ColorCode)
            {
                case "WhatsMyColor":
                    if (found != null)
                    {
                        return found.ColorCode;
                    }
                    else
                    {
                        AddToList(_ComputerName, _ColorCode);
                        return "Green";
                    }
                case "Green":
                case "Yellow":
                case "Red":
                    if (found != null && found.ColorCode != _ColorCode)
                    {
                        found.ColorCode = _ColorCode;
                        found.IsChanged = true;
                    }
                    else if (found == null)
                    {
                        AddToList(_ComputerName, _ColorCode);
                    }
                    break;
                default:
                    break;
            }
            return reply;
        }
        private static void AddToList(string _ComputerName, string _ColorCode)
        {
            ClientDataList.Add(new Models.ClientData
            {
                ComputerName = _ComputerName,
                ColorCode = _ColorCode,
                IsChanged = true
            });
        }

    }
}
