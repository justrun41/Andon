using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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

                // Receive the server response.

                // Buffer to store the response bytes.
                data = new byte[256];

                // String to store the response ASCII representation.
                string responseData = string.Empty;

                // Read the first batch of the TcpServer response bytes.
                int bytes = stream.Read(data, 0, data.Length);
                responseData = Encoding.ASCII.GetString(data, 0, bytes);
                if (responseData.Contains("Green") || responseData.Contains("Yellow") || responseData.Contains("Red"))
                {
                    ClientLogic.CurrentColor = responseData;
                }

                Debug.WriteLine("Received: {0}", responseData);
            }
            catch (ArgumentNullException e)
            {
                Debug.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Debug.WriteLine("SocketException: {0}", e);
            }
        }
    }
}
