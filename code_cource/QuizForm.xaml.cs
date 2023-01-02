using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.IO;
using System.Globalization;
using System.Xml.Linq;
using System.Text;
using System.Collections;
using System.Linq;
using System.Data;
using System.Drawing;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;

namespace programmation_systeme
{
    /// <summary>
    /// Logique d'interaction pour QuizForm.xaml
    /// </summary>
    public partial class QuizForm : Page
    {
        public QuizForm()
        {
            InitializeComponent();
            Save.Opacity = 0.3;
            Save.IsEnabled = false;
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Quiz quiz = new Quiz();
            this.NavigationService.Navigate(quiz);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            ValuesForm.Instance.MyQuestion = question.Text.ToString();

            question.Text = "";

            ValuesForm.Instance.QuestionList.Add(ValuesForm.Instance.MyQuestion);
            AddToList();

            Save.Opacity = 0.85;
            Save.IsEnabled = true;
        }


       


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ValuesForm.Instance.MyTitle = Values.Instance.Folder + "\\" + title.Text.ToString() + ".csv";
            Console.WriteLine(ValuesForm.Instance.MyTitle);

            StringBuilder form = new StringBuilder();

            foreach (var item in ValuesForm.Instance.QuestionList)
            {
                string row = item.ToString() ;
                form.AppendLine(row);
                File.AppendAllText(ValuesForm.Instance.MyTitle, form.ToString());
            }

            Quiz quiz = new Quiz();
            this.NavigationService.Navigate(quiz);
        }

        private void AddToList()
        {
            listView1.Items.Add(new ValuesForm()
            {
                a = ValuesForm.Instance.MyQuestion,
            });
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            listView1.Items.RemoveAt(listView1.Items.IndexOf(listView1.SelectedItem));
        }

        private void Add_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Ajoute la question.";
        }

        private void title_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Entre le titre de ton quiz.";
        }
        private void question_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Entre une question a ajouter dans ton quiz.";
        }
        private void listView1_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Liste des questions ajoutées.";
        }
        private void Cancel_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Retourne au Quiz.";
        }
        private void Save_MouseEnter(object sender, MouseEventArgs e)
        {
            Help.Text = "Sauvegarde le quiz.";
        }
    }

    public class ValuesForm
    {
        public List<string> QuestionList = new List<string>();
        public string a { get; set; }
        public string MyTitle { get; set; }
        public string MyQuestion { get; set; }
   
        public ValuesForm() { }
        public static readonly ValuesForm Instance = new ValuesForm();
    }


}
