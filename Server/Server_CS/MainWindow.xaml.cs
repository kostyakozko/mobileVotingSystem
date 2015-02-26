using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Server_CS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Listener listener;
        public MainWindow()
        {
            InitializeComponent();
            listener = new Listener(12345);
            listener.SocketAccepted +=listener_SocketAccepted;
            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            listener.Start();
        }

        void listener_SocketAccepted(System.Net.Sockets.Socket e)
        {
            Client client = new Client(e);
            client.Received += client_Received;
            client.Disconnected += client_Disconnected;
        }

        void client_Disconnected(Client sender)
        {
        }

        void client_Received(Client sender, byte[] data)
        {
        }
    }
}
