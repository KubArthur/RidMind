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

namespace programmation_systeme
{
    /// <summary>
    /// Logique d'interaction pour Exit.xaml
    /// </summary>
    public partial class Exit : Page
    {
        public Exit()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            this.NavigationService.Navigate(menu);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
