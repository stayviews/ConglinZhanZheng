using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace GameServer.Servers
{
    class Client
    {
        private Socket clientSocket;
        private Server server;
        Message msg = new Message();
        Client(Socket clientSocket, Server server)
        {
            this.clientSocket = clientSocket;
            this.server = server;
        }

        public void Start()
        {
            clientSocket.BeginReceive(null, SocketFlags.None, ReceiveCallBack, null);
        }

        void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                int count = clientSocket.EndReceive(ar);
                if (count == 0)
                {
                    Close();
                }

                msg.ReadMessage(count);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                Close();
            }
            clientSocket.BeginReceive(null, SocketFlags.None, ReceiveCallBack, null);
            
            Start();
        }

        void Close()
        {
            clientSocket.Close();
            server.RemoveClient(this);
        }
    }
}
