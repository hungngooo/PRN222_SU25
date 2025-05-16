using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
namespace ServerApp
{
     class Program
    {
        static void ProcessMessage(object param)
        {
            string data;
            int count;
            try
            {
                TcpClient client = (TcpClient)param;
                Byte[] bytes = new byte[256];
                NetworkStream stream = client.GetStream();
                while ((count = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, count);
                    Console.WriteLine($"Received: {data} at {DateTime.Now:t}");
                    data = $"{data.ToUpper()}";
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                    stream.Write(msg, 0, msg.Length);
                    Console.WriteLine($"Sent: {data}");
                }
                client.Close();
            }catch(Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
                Console.WriteLine("Waiting message...");
            }
            }
        
            
        
            static void ExecuteServer(string host, int port)
        {
            int count = 0;
            TcpListener server = null;
            try
            {
                Console.Title = "Server Application";
                IPAddress localAddr = IPAddress.Parse(host);
                server = new TcpListener(localAddr, port);
                server.Start();
                Console.WriteLine(new string('*',40));
                Console.WriteLine("Waiting for a connection...");
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine($"Number of client connected: {++count}");
                    Console.WriteLine(new string('*', 40));
                    Thread thread = new Thread(new ParameterizedThreadStart(ProcessMessage));
                    thread.Start(client);
                }
            }catch(Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message);
                Console.WriteLine("Waiting message...");
            }finally
            {
                server.Stop();
                Console.WriteLine("Server stopped. Press any key to exit.");
            }
            Console.Read();
        }
        static void Main(string[] args)
        {
            string host = "127.0.0.1";
            int port = 13000;
            ExecuteServer(host, port);
        }
    }
}
