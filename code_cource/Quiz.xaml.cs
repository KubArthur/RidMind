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
using System.Collections;
using System.IO;
using System.Globalization;
using System.Xml.Linq;

using System.Collections;
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
using System.Diagnostics;
using System.IO;

using System.Collections;

using System.Data;
using System.Drawing;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace programmation_systeme
{
    /// <summary>
    /// Logique d'interaction pour Quiz.xaml
    /// </summary>
    public partial class Quiz : Page
    {
        public Quiz()
        {
            InitializeComponent();
            Refresh();
        }



        public void Refresh()
        {
            listView1.Items.Clear();

            
            DirectoryInfo Main = new DirectoryInfo(Values.Instance.Folder);
            FileInfo[] Files = Main.GetFiles();

            foreach (FileInfo i in Files)
            {
                string nameFile = i.Name;
                nameFile = nameFile.Substring(0, nameFile.Length - 4);

                listView1.Items.Add(new ValuesQuiz()
                {
                    a = nameFile,
                });
            }
        }


        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            this.NavigationService.Navigate(menu);
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }


        private void Open_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Arguments = Values.Instance.Folder,
                FileName = "explorer.exe" };

        Process.Start(startInfo);
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            QuizForm quizform = new QuizForm();
            this.NavigationService.Navigate(quizform);
        }

        private void Return_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Retourne à l'écran d'accueil.";
        }

        private void Open_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Ouvre le dossier où sont stockés les quizzes.";
        }

        private void Create_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Crée un quiz à ajouter à la liste.";
        }

        private void listView1_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Liste des quizzes disponibles.";
        }

        private void Refresh_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Actualise la liste des quizzes.";
        }

        public class ValuesQuiz
        {
            public string a { get; set; }
        }
    }
}
