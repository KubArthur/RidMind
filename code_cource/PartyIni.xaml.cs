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
    /// Logique d'interaction pour PartyIni.xaml
    /// </summary>
    
    public partial class PartyIni : Page
    {
        Server runServer = new Server();
        public PartyIni()
        {
            
            //x.Test = 
            InitializeComponent();
            if (Values.Instance.Status == true)
            {
                Status.Content = "Lancer";
            }

            if (Values.Instance.Status == false)
            {
                Status.Content = "Rejoindre";
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Party party = new Party();
            this.NavigationService.Navigate(party);
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {

            Values.Instance.UserName = user.Text.ToString();
            Values.Instance.Port = int.Parse(port.Text);

            if (Values.Instance.Status == true)
            {
                PartyLobbyServer partyLobby = new PartyLobbyServer();
                this.NavigationService.Navigate(partyLobby);
            }

            if (Values.Instance.Status == false)
            {
                PartyLobbyClient partyLobby = new PartyLobbyClient();
                this.NavigationService.Navigate(partyLobby);
            }
        }

        private void Next_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Values.Instance.Status == true)
            {
                Help.Text = "Lance le lobby.";
            }

            if (Values.Instance.Status == false)
            {
                Help.Text = "Rejoins le lobby.";
            }
        }

        private void Return_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Retourne à Partie.";
        }

        private void Nom_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Entre ton nom.";
        }

        private void Port_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Entre le port de la partie.";
        }
    }
}
