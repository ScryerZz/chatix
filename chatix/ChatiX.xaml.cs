﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
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

namespace chatix
{
    /// <summary>
    /// Логика взаимодействия для ChatiX.xaml
    /// </summary>
    public partial class ChatiX : Page
    {
        static string userName;
        private const string host = "217.66.19.16";
        private const int port = 935;

        static TcpClient tcpClient; 

        public ChatiX()
        {
            InitializeComponent();
        }
    }
}
