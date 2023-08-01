using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AndonClient
{
    internal class UDPReceive
    {
        public static async void AsyncAwaitUDP()
        {
            string RecievedMessage = await ReceiveMessage();
            try
            {
                byte[] data = Convert.FromBase64String(RecievedMessage);
                string decodedString = Encoding.UTF8.GetString(data);
                Debug.WriteLine(decodedString);
                List<string> Split = decodedString.Split(',').ToList();
                if (decodedString.Contains("OK"))
                {
                    Helper.ServerIP = IPAddress.Parse(Split[1]);
                }
                else
                {
                    Debug.WriteLine(decodedString);
                }

     

            }
            catch (Exception)
            {
                throw;

            }

            AsyncAwaitUDP();
        }
        private static string dataRec = string.Empty;
        private static async Task<string> ReceiveMessage()
        {
            dataRec = string.Empty;
            await Task.Run(async () =>
            {
                using var udpClient = new UdpClient(11001);
                while (true)
                {
                    UdpReceiveResult receivedResult = await udpClient.ReceiveAsync();
                    dataRec = Encoding.ASCII.GetString(receivedResult.Buffer);
                    return dataRec;
                }
            });
            return dataRec;
        }
    }
}
