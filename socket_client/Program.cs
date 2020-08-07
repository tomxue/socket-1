using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;



namespace socket_client
{
    class Program
    {
        private static byte[] result = new byte[1024];

        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                clientSocket.Connect(new IPEndPoint(ip, 8885)); // 配置服务器IP与端口
                Console.WriteLine("Successfully connected to server!");


            }
            catch
            {
                Console.WriteLine("Failed to connect to server, please press Enter key to exit!");
                return;
            }

            int receiveLength = clientSocket.Receive(result);
            Console.WriteLine("Received message from server, the message: {0}", Encoding.ASCII.GetString(result, 0, receiveLength));
            // 通过clientSocket发送数据
            for(int i=0; i<10; i++)
            {
                try
                {
                    Thread.Sleep(1000);
                    string sendMessage = "Client send message Hello" + DateTime.Now;
                    clientSocket.Send(Encoding.ASCII.GetBytes(sendMessage));
                    Console.WriteLine("Send message to server: " + sendMessage);
                }
                catch
                {
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    break;
                }

            }

            Console.WriteLine("Sent completed, press Enter key to exit");
            Console.ReadLine();
        }
    }
}
