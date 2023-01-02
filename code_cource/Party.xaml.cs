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
    /// Logique d'interaction pour Party.xaml
    /// </summary>
    public partial class Party : Page
    {
        public Party()
        {
            InitializeComponent();
            Values.Instance.ScoreList.Clear();
            Values.Instance.UserList.Clear();
        }

        private void Join_Click(object sender, RoutedEventArgs e)
        {
            Values.Instance.Status = false;
            PartyIni partycreate = new PartyIni();
            this.NavigationService.Navigate(partycreate);
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            Values.Instance.Status = true;
            PartyIni partycreate = new PartyIni();
            this.NavigationService.Navigate(partycreate);
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            this.NavigationService.Navigate(menu);
        }

        private void Join_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Rejoins une partie déjà créée.";
        }

        private void Create_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Crée une partie composée de 8 joueurs max.";
        }

        private void Return_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Retourne à l'écran d'accueil.";
        }
    }
}
