using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;

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
                ReadConnectionString();
                ClientDataList = new();
                ChangedClientDataList = new();
                TCPReceiver.BeginListen();
                Started = true;
            }
        }

        private static void ReadConnectionString()
        {

            XmlDocument xmlServerConn = new();
            xmlServerConn.Load($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\Settings.xml");  //remove all but settings for live
            XmlNodeList nodes = xmlServerConn.DocumentElement.SelectNodes("/Connect");
            foreach (XmlNode node in nodes)
            {
               Helper.ConnectionString = $"mongodb://andon-mtas:{node.SelectSingleNode("Secure").InnerText}@andon-mtas.mongo.cosmos.azure.com:10255/?ssl=true&retrywrites=false&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@andon-mtas@";
                Debug.Write(Helper.ConnectionString);
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
                        SendToDB();
                        return "Green";
                    }
                case "Green":
                case "Yellow":
                case "Red":
                    if (found != null && found.ColorCode != _ColorCode)
                    {
                        found.ColorCode = _ColorCode;
                        AddToList(ChangedClientDataList, _ComputerName, _ColorCode);
                        SendToDB();
                    }
                    else if (found == null)
                    {
                        AddToList(ClientDataList, _ComputerName, _ColorCode);
                        AddToList(ChangedClientDataList, _ComputerName, _ColorCode);
                        SendToDB();
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

        private static void SendToDB()
        {
            if (ChangedClientDataList.Count == 1)
            {
                MongoDBConnect.SendToDB(ChangedClientDataList);
               // ChangedClientDataList.Clear();
            }
        }
        private static void UpdateCoordinates(string _ComputerName, int XCoor, int YCoor)  //for future when Overview is written.  This doesn't work, probably.  Use foreach
        {
            _ = ClientDataList.Where(x => x.ComputerName == _ComputerName).Select(x => { x.XCoordinate = XCoor; x.YCoordinate = YCoor; x.CoordinatesChanged = true; return 0; });
        }

        //update database on a timer from the ChangedClientDataList and from coordinates of ClientDataList, then clear out that list if successful
        // Add to database table for changes and update color in database table for AndonClients
        //after update, set CoordinatesChanged to false

    }
}
