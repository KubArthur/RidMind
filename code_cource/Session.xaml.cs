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
    public partial class Session : Page
    {
        Client runInv = new Client();
        SessionServer RunSessionServer = new SessionServer();
        SessionClient RunSessionCient = new SessionClient();

        private bool Running = true;

        private DispatcherTimer Timer;
        private int time = int.Parse(Values.Instance.Settings[3]);

        public Session()
        {

            if (Values.Instance.Status == true)
            {
                RunSessionServer.InitiliazeQuiz();
            }

            ValuesSession.Instance.CurrentTurn = 0;

            // while getTime + t !=> synctime
            //runInv.SendString("Q");

            InitializeComponent();
            ValuesSession.Instance.Choice = "0";
            Titre.Text = Values.Instance.Settings[0];
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Tick += Session_Timer;
            

            if (Values.Instance.Status == false)
            {
                new Thread(() =>
                {
                    runInv.RequestLoop();
                }).Start();

                new Thread(() =>
                {
                    while (Running)
                    {
                        if (time == int.Parse(Values.Instance.Settings[3]) && ValuesSession.Instance.CurrentTurn < int.Parse(Values.Instance.Settings[2]))
                        {
                            runInv.SendString("Q-");

                            Thread.Sleep(500);

                            this.Dispatcher.Invoke(() => { Refresh(); });

                            Timer.Start();

                            Thread.Sleep(1000);
                        }
                    }
                }).Start();
            }

            if (Values.Instance.Status == true)
            {
                new Thread(() =>
                {
                    while (Running)
                    {
                        if (time == int.Parse(Values.Instance.Settings[3]) && ValuesSession.Instance.CurrentTurn < int.Parse(Values.Instance.Settings[2]))
                        {
                            this.Dispatcher.Invoke(() => { Refresh(); });

                            Timer.Start();

                            Thread.Sleep(1000);
                        }
                    }
                }).Start();
            }

            new Thread(() =>
            {
                while (Running)
                {
                    this.Dispatcher.Invoke(() => { alpha(); });
                }
            }).Start();
            
        }

        public void alpha()
        {
            if (ValuesSession.Instance.CurrentTurn == int.Parse(Values.Instance.Settings[2]))
            {
                Running = false;
                SessionEnd sessionend = new SessionEnd();
                this.NavigationService.Navigate(sessionend);
            }
        }

        private void C1_Click(object sender, RoutedEventArgs e)
        {
            ValuesSession.Instance.Choice = "1";
        }

        private void C2_Click(object sender, RoutedEventArgs e)
        {
            ValuesSession.Instance.Choice = "0";
        }

        public void Refresh()
        {

            Question.Text = ValuesSession.Instance.CurrentQuestion;
        }

        private void Titre_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Titre du quiz en cours.";
        }

        private void Question_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Question en cours.";
        }

        private void Chrono_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Rythme la session.";
        }

        private void Yes_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Sélectionne la réponse Oui.";
        }

        private void No_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Sélectionne la réponse Non.";
        }

        private void GuessNumber_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Devine le nombre de Oui qui va être choisit.";
        }
        

        private void GuessNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var se = sender as ComboBox;

            ValuesSession.Instance.Guess = se.SelectedItem as string;
        }

        private void GuessNumber_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();

            data.Add("0");
            for (int i = 1; i < int.Parse(Values.Instance.Settings[1]) + 1; i++)
            {
                data.Add(i.ToString());
            }

            var combo = sender as ComboBox;

            combo.ItemsSource = data;
            combo.SelectedIndex = 0;
        }

        public void Session_Timer(object sender, EventArgs e)
        {
            if (time > 0)
            {
                time--;
                Chrono.Text = string.Format("{1}", time / 60, time % 60); // refresh print envoie requete + get answer
            }

            else if (Values.Instance.Status == true)
            {
                Timer.Stop();

                RunSessionServer.ServerAnswer();

                Thread.Sleep(3000);
                RunSessionServer.ServerGet(ValuesSession.Instance.CurrentTurn);

                Chrono.Text = "Question suivante...";
                Thread.Sleep(2000);
                ValuesSession.Instance.Update = false;
                ValuesSession.Instance.CurrentTurn++;
                ValuesSession.Instance.CurrentQuestion = ValuesSession.Instance.QuestionList[ValuesSession.Instance.CurrentTurn];

                time = int.Parse(Values.Instance.Settings[3]);
            }

            else if (Values.Instance.Status == false)
            {
                Timer.Stop();
                RunSessionCient.ClientAnwser();
                Chrono.Text = "Question suivante...";

                while (ValuesSession.Instance.Update == false)
                {
                    runInv.SendString("A-");
                    Thread.Sleep(10);
                }

                
                Thread.Sleep(2000);
                ValuesSession.Instance.Update = false;

                ValuesSession.Instance.CurrentTurn++;


                
                time = int.Parse(Values.Instance.Settings[3]);
            }
        }

        public void Session_Client()
        {
            Timer.Stop();

            RunSessionCient.ClientAnwser();

            time = int.Parse(Values.Instance.Settings[3]);
        }

        public void Session_Server()
        {
            Timer.Stop();
            RunSessionServer.ServerAnswer();

            //ValuesSession.Instance.CurrentTurn++;

            time = int.Parse(Values.Instance.Settings[3]);
        }
        /// <summary>
        /// Response
        /// </summary>




        /// && guess == ValuesSession.Instance.GuessList[0]
        public void SessionState()
        {
            // Refresh page avancement dans le quiz
        }


    }

    class ValuesSession
    {
        public List<string> MarkPoint { get; set; } = new List<string>();
        public List<string> QuestionList { get; set; } = new List<string>();
        public List<string> UserResponse { get; set; } = new List<string>();
        public string Point { get; set; }
        public string Match { get; set; }
        public int CurrentTurn { get; set; }
        public string CurrentQuestion { get; set; }
        public string Choice { get; set; }
        public string Guess { get; set; }
        public bool Update { get; set; }
        public ValuesSession() { }
        public static readonly ValuesSession Instance = new ValuesSession();
    }
}

