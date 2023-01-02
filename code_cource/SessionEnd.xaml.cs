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
using System.Diagnostics;
using System.IO;
using System.Globalization;
using System.Xml.Linq;

using System.Collections;

using System.Data;
using System.Drawing;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Threading;

namespace programmation_systeme
{
    public partial class SessionEnd : Page
    {
        Client runInv = new Client();
        Server runServer = new Server();


        public SessionEnd()
        {

            InitializeComponent();

            if (Values.Instance.Status == false)
            {
                runInv.SendString("S-");
                Thread.Sleep(1000);
                Refresh();
            }

            if (Values.Instance.Status == true)
            {
                foreach (var item in Values.Instance.ScoreList)
                {
                    listView1.Items.Add(new ValuesSessionEnd()
                    {
                        a = item,
                    });
                }
            }
        }

        private void ReturnMenu_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Retourne au menu.";
        }

        private void ListView_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Tableau des scores de la session.";
        }

        private void BackToLobby_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Relance une partie.";
        }

        private void BackToLobby_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ReturnMenu_Click(object sender, RoutedEventArgs e)
        {

            if (Values.Instance.Status == true)
            {
                runServer.CloseAllSockets();
            }

            if (Values.Instance.Status == false)
            {
                runInv.Exit();
            }

            Menu menu = new Menu();
            this.NavigationService.Navigate(menu);
        }


        private void Refresh()
        {
            

            listView1.Items.Clear();

            for (int i = 0; i < ValuesSessionEnd.Instance.FinalList.Count; i++)
            {
                listView1.Items.Add(new ValuesClient()
                {
                    a = ValuesSessionEnd.Instance.FinalList[i],
                });
            }

            runInv.SendString("I");
        }



    }
    class ValuesSessionEnd
    {
        public List<string> FinalList { get; set; } = new List<string>();
        public string a { get; set; }
        public ValuesSessionEnd() { }
        public static readonly ValuesSessionEnd Instance = new ValuesSessionEnd();
    }         
    

       
}
