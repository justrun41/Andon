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
        public static string ClientDataSorter(string data)
        {
            string[] s = data.Split(',');
            string _ComputerName = s[0].Remove(0, s[0].IndexOf(':'));
            string? reply = null;
            try
            {
                if (data.Contains("ColorCode"))
                {
                    
                    ClientDataList.Add(new Models.ClientData
                    {
                        ComputerName = _ComputerName, 
                        ColorCode = s[1].Remove(0, s[1].IndexOf(':'))
                    });
                }
                else if (data.Contains("WhatsMyColor"))
                {
                    Models.ClientData found = ClientDataList.Find(x => x.ComputerName == _ComputerName);
                    reply = found.ColorCode;
                }
                return reply;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
