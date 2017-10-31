using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
namespace TCPserver
{
    class Program
    {

        
        static void Main(string[] args)
        {
            StartServerAsync();
            Console.ReadKey();
        }
        static  Message msg = new Message();
        static void StartServerAsync()
        {
            Socket socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipEndPoint = new IPEndPoint(iPAddress, 8804);
            socketServer.Bind(ipEndPoint);
            socketServer.Listen(0);
            socketServer.BeginAccept(AcceptCallBack, socketServer);
        }
       
        static void AcceptCallBack(IAsyncResult ar)
        {
            Socket socketServer = ar.AsyncState as Socket;
            Socket clientSocket = socketServer.EndAccept(ar);

            string msgStr = "Hello this is sever \n";
            byte[] data = Encoding.UTF8.GetBytes(msgStr);
            clientSocket.Send(data);

            clientSocket.BeginReceive(msg.data, msg.startIndex, msg.RemainSize, SocketFlags.None, ReceiveCallBack, clientSocket);

            socketServer.BeginAccept(AcceptCallBack, socketServer);
        }


        static void ReceiveCallBack(IAsyncResult ar)
        {
            Socket clientSocket=null;
            try
            {
                clientSocket = ar.AsyncState as Socket;
                int count = clientSocket.EndReceive(ar);
                if (count ==0)
                {
                    clientSocket.Close();
                    return;
                }

                msg.AddIndex(count);
                msg.ReadMessage();
               
                clientSocket.BeginReceive(msg.data, msg.startIndex, msg.RemainSize, SocketFlags.None, ReceiveCallBack, clientSocket);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                if (clientSocket!=null)
                {
                    clientSocket.Close();
                }
            }
        }
    }
}
