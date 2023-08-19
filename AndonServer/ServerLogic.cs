using System;
using System.Collections.Generic;
using System.Linq;

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
        public static void ClientDataSorter(string data)
        {
            try
            {
                List<string> TempList = data.Split(',').ToList();
                ClientDataList.Add(new Models.ClientData
                {
                    ComputerName = TempList[0],  //pulls in ComputerName:{ComputerName}   alter?
                                                 //ColorCode:Green
                    ColorCode = TempList[1]
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
