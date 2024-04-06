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

namespace chatix
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void StartChating_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new ChatiX());
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.Shutdown();
        }

        private void DoubleAnimation_Completed_StCH(object sender, EventArgs e)
        {
            NavigationService.Navigate(new ChatiX());
        }

        private void DoubleAnimation_Completed_Ex(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
