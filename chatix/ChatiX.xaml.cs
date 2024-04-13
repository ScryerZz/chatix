using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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

namespace chatix
{
    /// <summary>
    /// Логика взаимодействия для ChatiX.xaml
    /// </summary>
    public partial class ChatiX : Page
    {
        int outmessage = 0;
        int inmessage = 0;

        string host = "217.66.19.16";
        int port = 935;

        TcpClient client = null;
        NetworkStream stream = null;

        public ChatiX()
        {
            InitializeComponent();
        }

        public void Connect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                client = new TcpClient(host, port);
                stream = client.GetStream();
                Dispatcher.BeginInvoke(new Action(() => ClientLog.Items.Add("Соединение установлено.")));
            }
            catch (Exception ex)
            {
                ClientLog.Items.Add(ex.Message);
            }
            if (client != null)
            {
                Thread listenThread = new Thread(() => listen());
                listenThread.Start();
            }
        }

        void listen()
        {
            try
            {
                while (true)
                {
                    byte[] data = new byte[64];
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);
                    string message = builder.ToString();
                    Dispatcher.BeginInvoke(new Action(() => ClientLog.Items.Add("Сервер: " + message)));
                    Dispatcher.BeginInvoke(new Action(() => CountIn()));
                }
            }

            catch (Exception ex)
            {
                Dispatcher.BeginInvoke(new Action(() => ClientLog.Items.Add(ex.Message)));
            }
            finally
            {
                stream.Dispose();
                stream.Close();
                client.Dispose();
                client.Close();
            }
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            string message = Message.Text;
            message = String.Format("{0}: {1}", Name.Text, message);
            byte[] data = Encoding.Unicode.GetBytes(message);
            if (client != null && client.Connected)
            {
                stream.Write(data, 0, data.Length);
                countout.Text = Convert.ToString(outmessage = outmessage + 1);
                Dispatcher.BeginInvoke(new Action(() => ClientLog.Items.Add(message)));
            }
            else Dispatcher.BeginInvoke(new Action(() => ClientLog.Items.Add("Соединение не было установлено.")));
        }

        void CountIn()
        {
            countin.Text = Convert.ToString(inmessage = inmessage + 1);
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            if (client != null)
            {
                stream.Dispose();
                stream.Close();
                client.Close();
                client.Dispose();
                ClientLog.Items.Add("Соединение закрыто.");
            }
            else Dispatcher.BeginInvoke(new Action(() => ClientLog.Items.Add("Соединение не было установлено.")));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.GoBack();
        }

        private void DoubleAnimation_Completed_Back(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
