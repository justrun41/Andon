using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace AndonServer
{
    internal static class ServerLogic
    {
        private static bool Started = false;
        private static List<Models.ClientData>? ClientDataList { get; set; }
        private static List<Models.ClientData>? ChangedClientDataList { get; set; }
        public static void Listener()
        {
            if (!Started) 
            {
                Helper.GetIPAddress();
                ClientDataList = new();
                ChangedClientDataList = new();
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
                        AddToList(ClientDataList, _ComputerName, "Green");
                        AddToList(ChangedClientDataList, _ComputerName, "Green");
                        return "Green";
                    }
                case "Green":
                case "Yellow":
                case "Red":
                    if (found != null && found.ColorCode != _ColorCode)
                    {
                        found.ColorCode = _ColorCode;
                        AddToList(ChangedClientDataList, _ComputerName, _ColorCode);
                    }
                    else if (found == null)
                    {
                        AddToList(ClientDataList, _ComputerName, _ColorCode);
                        AddToList(ChangedClientDataList, _ComputerName, _ColorCode);
                    }
                    break;
                default:
                    break;
            }
            return reply;
        }
        private static void AddToList(List<Models.ClientData> list, string _ComputerName, string _ColorCode)
        {
            list.Add(new Models.ClientData
            {
                ComputerName = _ComputerName,
                ColorCode = _ColorCode
            });
        }
        private static void UpdateCoordinates(string _ComputerName, int XCoor, int YCoor)  //for future when Overview is written.
        {
            _ = ClientDataList.Where(x => x.ComputerName == _ComputerName).Select(x => { x.XCoordinate = XCoor; x.YCoordinate = YCoor; return 0; });
        }

        //update database on a timer from the changedclientdatalist, then clear out that list.  Add to database table for changes and update color in database table for AndonClients

    }
}
