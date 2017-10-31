using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using CommonData;


namespace GameServer.Servers
{
    class Server
    {
        private IPEndPoint ipEndpoint;
        private Socket serverSocket;
        private List<Client> clientList;

        Server(string IPstr, int Iport)
        {
            SetIPandIport(IPstr, Iport);
        }
        void SetIPandIport(string IPstr, int Iport)
        {
            ipEndpoint = new IPEndPoint(IPAddress.Parse(IPstr), Iport);
        }
        void Start()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(ipEndpoint);
            serverSocket.Listen(0);
            serverSocket.BeginAccept(AcceptCallBack, null);
        }

        void AcceptCallBack(IAsyncResult ar)
        {
            Socket clientSocket = serverSocket.EndAccept(ar);
            Client client = new Client(clientSocket,this);
        }

        public void RemoveClient(Client client)
        {
            lock (clientList)
            {
                clientList.Remove(client);
            }
        }

        public void SendRespones(RequestType rt ,Client client)
        {

        }
    }
}