using System;
using System.Net;
using System.Net.Sockets;

namespace Server_CS
{
    //One client
    class Client
    {
        //Client name - probably should be unique id or smth like this
        public string Name
        {
            get;
            set;
        }

        //Client IP address - should not be used as identifier
        public IPEndPoint EndPoint
        {
            get;
            private set;
        }

        //Client socket
        public Socket sock;

        public Client(Socket accepted)
        {
            sock = accepted;
            Name = Guid.NewGuid().ToString(); //not sure if it's right naming method
            EndPoint = (IPEndPoint)sock.RemoteEndPoint;
            sock.BeginReceive(receiveBuffer, 0, BUFFER_SIZE, 0, callback, null); //zero offset, no flags
        }

        void callback(IAsyncResult ar)
        {
            try
            {
                int rec = sock.EndReceive(ar);

                if (Received != null && rec > 0)
                {
                    Received(this, receiveBuffer);
                }
 
                sock.BeginReceive(receiveBuffer, 0, BUFFER_SIZE, 0, callback, null);
            }
            catch
            {

                Close();

                if (Disconnected != null)
                {
                    Disconnected(this);
                }
            }
        }

        public void Close()
        {
            sock.Close();
            sock.Dispose();
        }

        public delegate void ClientReceivedHandler(Client sender, byte[] data);
        public delegate void ClientDisconnectedHandler(Client sender);

        public event ClientReceivedHandler Received;
        public event ClientDisconnectedHandler Disconnected;

        private const int BUFFER_SIZE = 8192;
        private byte[] receiveBuffer = new byte[BUFFER_SIZE];
    }
}
