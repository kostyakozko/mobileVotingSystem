using System;
using System.Net;
using System.Net.Sockets;

namespace Server_CS
{
    //Socket listener
    class Listener
    {
        //socket for client
        Socket listenerSocket;
        
        //is listener working now? should be only get directly
        public bool isListening
        {
            get;
            private set;
        }

        //connection port/ shoult be only get directly
        public int Port
        {
            get;
            private set;
        }

        public Listener(int port)
        {
            Port = port;
            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //IPv4
        }

        public void Start()
        {
            if (isListening)
            {
                return;
            }

            listenerSocket.Bind(new IPEndPoint(IPAddress.Any, Port)); //probably should be new IPEndPoint(IPAddress.Any, 0) to let Windows determine free port
            listenerSocket.Listen(0); //0 means unlimited pending queue

            listenerSocket.BeginAccept(callback, listenerSocket);
            isListening = true;
        }

        public void Stop()
        {
            if (!isListening)
                return;

            listenerSocket.Close();
            listenerSocket.Dispose();
            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        //Magic happens here
        void callback(IAsyncResult ar)
        {
            try
            {
                Socket s = this.listenerSocket.EndAccept(ar);

                if (SocketAccepted != null)
                {
                    SocketAccepted(s);
                }

                this.listenerSocket.BeginAccept(callback, this.listenerSocket);
            }
            catch
            {

            }
        }

        public delegate void SocketAcceptedHandler(Socket e);
        public event SocketAcceptedHandler SocketAccepted;
    }
}
