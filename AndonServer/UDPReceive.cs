﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AndonServer
{
    internal static class UDPReceive
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
                if (decodedString.Contains("Client"))
                {
                    //Debug.WriteLine($"{Split[1]}");
                    Helper.GetIPAddress();
                    UDPSend.SendData(IPAddress.Parse(Split[1]), $"OK,{Helper.ComputerIP}");
                }
                else
                {

                }

                //else if (decodedString.Contains("OK"))
                //{
                //    Helper.ServerIP = IPAddress.Parse(Split[1]);
                //}

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
        private static string dataRec = string.Empty;
        private static async Task<string> ReceiveMessage()
        {
            dataRec = string.Empty;
            await Task.Run(async () =>
            {
                using var udpClient = new UdpClient(11000);
                while (true)
                {
                    UdpReceiveResult receivedResult = await udpClient.ReceiveAsync();
                    Debug.WriteLine(receivedResult.RemoteEndPoint);
                    dataRec = Encoding.ASCII.GetString(receivedResult.Buffer);
                    return dataRec;
                }
            });
            return dataRec;
        }
        
    }
}