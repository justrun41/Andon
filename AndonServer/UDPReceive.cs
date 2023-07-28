using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AndonServer
{
    internal class UDPReceive
    {
        public UDPReceive()
        {
            AsyncAwaitUDP();
        }

        private async void AsyncAwaitUDP()
        {
            string RecievedMessage = await ReceiveMessage();


            //List<string> Split = RecievedMessage.Split(',').ToList();
            try
            {
                byte[] data = Convert.FromBase64String(RecievedMessage);
                string decodedString = Encoding.UTF8.GetString(data);
                Debug.WriteLine(decodedString);
                //if (!Listbox_Devices.Items.Contains(decodedString) | !Listbox_IP.Items.Contains(Split[1]))
                //{
                //    Listbox_Devices.Items.Add(decodedString);
                //    Listbox_IP.Items.Add(Split[1]);
                //}
            }
            catch (Exception)
            {
                throw;

            }

            AsyncAwaitUDP();
        }
        string dataRec = string.Empty;
        private async Task<string> ReceiveMessage()
        {
            dataRec = string.Empty;
            await Task.Run(async () =>
            {
                using var udpClient = new UdpClient(7676);
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
