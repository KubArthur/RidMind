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
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.IO;
using System.Globalization;
using System.Xml.Linq;

using System.Collections;

using System.Data;
using System.Drawing;
using System.ComponentModel;
using System.Collections.ObjectModel;



namespace programmation_systeme
{
    public class ValuesServer
    {
        public string a { get; set; }
    }

    /// <summary>
    /// Logique d'interaction pour PartyLobbyServer.xaml
    /// </summary>
    public partial class PartyLobbyServer : Page
    {
        Server runServer = new Server();
        bool Running = true;
        public PartyLobbyServer()
        {
            InitializeComponent();

            Values.Instance.UserList.Add(Values.Instance.UserName);

            listView1.Items.Add(new ValuesServer()
            {
                a = Values.Instance.UserList[0],
            });


            for (int i = 1; i < 6; i++)
            {
                Values.Instance.Settings.Add("");
            }

            runServer.SetupServer();

            if (Running)
            {
                new Thread(() =>
                {
                    while (Running)
                    {
                        this.Dispatcher.Invoke(() => { Refresh(); });
                        Thread.Sleep(1000);
                    }

                }).Start();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            runServer.CloseAllSockets();
            Menu menu = new Menu();
            this.NavigationService.Navigate(menu);
        }

        public void Refresh()
        {
            listView1.Items.Clear();

            for (int i = 0; i < Values.Instance.UserList.Count; i++)
            {
                listView1.Items.Add(new ValuesServer()
                {
                    a = Values.Instance.UserList[i],
                });
            }
        }

        

        private void NameFile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var se = sender as ComboBox;
            Values.Instance.Settings[0] = se.SelectedItem as string;
            string file = se.SelectedItem as string;
            Values.Instance.File = file + ".csv";
        }

        private void PlayerNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var se = sender as ComboBox;
            Values.Instance.Settings[1] = se.SelectedItem as string;
        }

        private void TurnNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var se = sender as ComboBox;
            Values.Instance.Settings[2] = se.SelectedItem as string;
        }

        private void Timer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var se = sender as ComboBox;
            Values.Instance.Settings[3] = se.SelectedItem as string;
        }

        private void NameFile_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            DirectoryInfo Main = new DirectoryInfo(Values.Instance.Folder);
            FileInfo[] Files = Main.GetFiles();
            
            foreach(FileInfo i in Files)
            {
                string nameFile = i.Name;
                nameFile = nameFile.Substring(0, nameFile.Length - 4);
                data.Add(nameFile);          
            }

            var combo = sender as ComboBox;
            combo.ItemsSource = data;
            combo.SelectedIndex = 0;
        }

        private void PlayerNumber_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();

            for (int i = 2; i < 9; i++)
            {
                data.Add(i.ToString());
            }

            var combo = sender as ComboBox;
            combo.ItemsSource = data;
            combo.SelectedIndex = 0;
        }

        private void TurnNumber_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();

            for (int i = 1; i < 6; i++)
            {
                int a = i * 5;
                data.Add(a.ToString());
            }
            
            var combo = sender as ComboBox;
            combo.ItemsSource = data;
            combo.SelectedIndex = 0;
        }

        private void Timer_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();

            for (int i = 1; i < 4; i++)
            {
                int a = i * 15;
                data.Add(a.ToString());
            }

            var combo = sender as ComboBox;
            combo.ItemsSource = data;
            combo.SelectedIndex = 0;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            int check = int.Parse(Values.Instance.Settings[1]);

            if (check == Values.Instance.UserList.Count)
            {
                Values.Instance.Settings[4] = "1";
                Running = false;
                Session session = new Session();
                this.NavigationService.Navigate(session);
            }

            else
            {
                MessageBox.Show("Problème nombre joueur");
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
            Help.Text = "Démarre la session.";
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
}
