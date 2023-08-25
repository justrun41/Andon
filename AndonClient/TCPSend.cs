using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AndonClient
{
    internal static class TCPSend
    {
        
        public static void Connect(string server, string message)
        {
            try
            {

                int port = 13076;
                using TcpClient client = new(server, port);
                byte[] data = Encoding.ASCII.GetBytes(Convert.ToBase64String(Encoding.UTF8.GetBytes(message)));
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);

                Debug.WriteLine("Sent: {0}", message);
                data = new byte[256];
                string responseData = string.Empty;

                int bytes = stream.Read(data, 0, data.Length);
                responseData = Encoding.ASCII.GetString(data, 0, bytes);

                if (responseData.Contains("Green") || responseData.Contains("Yellow") || responseData.Contains("Red"))
                {
                    ClientLogic.CurrentColor = responseData;
                }

                Debug.WriteLine($"Received: {responseData}");
            }
            catch (ArgumentNullException e)
            {
                Debug.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                if (Helper.Retry)
                {
                    Helper.Retry = false;
                    Debug.WriteLine("SocketException: {0}", e);
                    Debug.WriteLine("Failed to connect");
                    MessageBox.Show($"{e.Message}  - Retrying once more.");
                    Thread.Sleep(10000);
                    Connect(server, message);

                }
                else
                {
                    MessageBox.Show("Failed to Connect.  Contact IT.");
                    Environment.Exit(0);
                }
                
            }

        }
    }
}
