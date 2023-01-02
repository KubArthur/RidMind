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
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.ComponentModel;

namespace programmation_systeme
{
    /// <summary>
    /// Logique d'interaction pour PartyLobbyClient.xaml
    /// </summary>
    public partial class PartyLobbyClient : Page
    {
        Client runInv = new Client();
        bool Running = true;
        public PartyLobbyClient()
        {
            

            runInv.ConnectToServer();
            runInv.SendString("U-" + Values.Instance.UserName);
            Thread.Sleep(300);

            InitializeComponent();

            Start.Opacity = 0.3;
            Start.IsEnabled = false;

            for (int i = 1; i < 6; i++)
            {
                Values.Instance.Settings.Add("");
            }

            new Thread(() =>
            {
                runInv.RequestLoop();
            }).Start();

            new Thread(() =>
            {
                while (Running)
                {
                    this.Dispatcher.Invoke(() => { Refresh(); });
                    Thread.Sleep(500);
                }
            }).Start();

            new Thread(() =>
            {
                while (Running)
                {
                    this.Dispatcher.Invoke(() => { alpha(); });
                }
            }).Start();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Running = false;
            Thread.Sleep(1000);
            runInv.Exit();
            Menu menu = new Menu();
            this.NavigationService.Navigate(menu);
        }

        private void Refresh()
        {
            runInv.SendString("U");

            listView1.Items.Clear();

            for (int i = 0; i < Values.Instance.UserList.Count; i++)
            {
                listView1.Items.Add(new ValuesClient()
                {
                    a = Values.Instance.UserList[i],
                });
            }

            runInv.SendString("I");

            nf.Text = Values.Instance.Settings[0];
            np.Text = Values.Instance.Settings[1];
            nq.Text = Values.Instance.Settings[2];
            nq2.Text = Values.Instance.Settings[3];
        }

        public void alpha()
        {
            if (Values.Instance.Settings[4] == "1")
            {
                Running = false;
                Session session = new Session();
                this.NavigationService.Navigate(session);
            }
        }

        private void TurnNumber_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Nombre de questions posées.";
        }

        private void PlayerNumber_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Nombre de joueur max dans la session.";
        }

        private void NameFile_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Nom du quiz sélectionné.";
        }

        private void Timer_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Temps pour pouvoir répondre.";
        }

        private void Start_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Seul l'hôte peut démarrer la session.";
        }

        private void Cancel_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Retourne à l'écran d'accueil.";
        }

        private void PlayerList_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Joueur présent dans la session.";
        }

        private void Settings_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Paramètres de la session.";
        }
    }

    public class ValuesClient
    {
        public string a { get; set; }
    }
}
