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
    /// Logique d'interaction pour Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Party_Click(object sender, RoutedEventArgs e)
        {
            Party party = new Party();
            this.NavigationService.Navigate(party);
        }

        private void Quiz_Click(object sender, RoutedEventArgs e)
        {
            Quiz quiz = new Quiz();
            this.NavigationService.Navigate(quiz);
        }

   

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Exit exit = new Exit();
            this.NavigationService.Navigate(exit);
        }

        private void ButtonParty_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Démarre une partie locale composée de 8 joueurs max.";
        }

        private void ButtonQuiz_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Affiche les quizzes disponibles.";
        }

        private void ButtonExit_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Ferme l'application.";
        }
    }
}
