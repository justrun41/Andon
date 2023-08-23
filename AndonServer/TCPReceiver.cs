using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace AndonServer
{
    internal static class TCPReceiver
    {
        public static void BeginListen()
        {
           Task.Run(Listener);
        }

        private static void Listener()
        {
            
            TcpListener? server = null;
            try
            {

                Int32 port = 13076;
                IPAddress? localAddr = Helper.ComputerIP;
                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                byte[] bytes = new Byte[256];
                string? data = null;

                while (true)
                {
                    Debug.Write("Waiting for a connection... ");

                    using TcpClient client = server.AcceptTcpClient();
                    Debug.WriteLine("Connected!");

                    data = null;

                    NetworkStream stream = client.GetStream();
                    
                    int i;

                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        
                        // Translate data bytes to a ASCII string.
                        //data = Encoding.ASCII.GetString(bytes, 0, i);
                        data = Encoding.UTF8.GetString(Convert.FromBase64String(Encoding.ASCII.GetString(bytes, 0, i)));
                        Debug.WriteLine($"Received: {data}");
                        string reply = ServerLogic.ClientDataSorter(data);
                        if (reply != null) 
                        { 
                            data = reply; 
                        }

                        // Process the data sent by the client.
                        //data = data.ToUpper();

                        byte[] msg = Encoding.ASCII.GetBytes(data);
                        

                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);
                        Debug.WriteLine($"Sent: {data}");
                    }
                }
            }
            catch (SocketException e)
            {
                Debug.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server.Stop();
            }
        }

    }
}
