using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
namespace TCPclient
{
    class Program
    {
        static void Main(string[] args)
        {

            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8804));

            //接收数据
            byte[] data = new Byte[1024];
            int count = clientSocket.Receive(data);

            string msg = Encoding.UTF8.GetString(data, 0, count);
            Console.Write(msg);

            //发送数据
            while (true)
            {
                string s = Console.ReadLine();

                clientSocket.Send(Message.GetBytes(s));
            }

            Console.ReadKey();
            clientSocket.Close();
        }
    }
}
