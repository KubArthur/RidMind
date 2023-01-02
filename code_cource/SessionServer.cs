using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    class SessionServer
    {
        public void ServerAnswer()
        {
            // élément à envoyer au serv pour déduire score (button + guess)
            string packet = Values.Instance.UserName + "," + ValuesSession.Instance.Choice + "," + ValuesSession.Instance.Guess;
            ValuesSession.Instance.UserResponse.Add(packet);
        }

        public void ServerGet(int stateTurn) //ValuesSession.Instance.CurrentTurn
        {
            int correctGuess = 0;
            int yesNumber = 0;
            bool match;
            // ValuesSession.Instance.ClientResponse (P?)
            // ValuesSession.Instance.ScoreList
            for (int i = 0; i != Values.Instance.UserList.Count(); i++)
            {
                
                var listAnswer = ValuesSession.Instance.UserResponse[i].Split(',').ToList();

                //string userName = listAnswer[0];
                string choice = listAnswer[1];
                //string guess = listAnswer[2];

                if (choice == "1") //j state
                {
  
                    yesNumber++;
                }        
            }
            
            if (yesNumber == 0) //j state
            {
              
                match = false;
                ValuesSession.Instance.Point = yesNumber.ToString() + "," + "1";
                for (int i = 0; i != Values.Instance.UserList.Count(); i++)
                {
                    var listAnswer = ValuesSession.Instance.UserResponse[i].Split(',').ToList();

                    int choice = int.Parse(listAnswer[1]); //error si no press

                    if (yesNumber < choice)
                    {
                        yesNumber = choice;
                    }
                }
            }

            else
            {
                match = true;
                ValuesSession.Instance.Point = yesNumber.ToString() + "," + "2";
            }
            

            

            for (int i = 0; i < Values.Instance.UserList.Count(); i++)
            {
                var listAnswer = ValuesSession.Instance.UserResponse[i].Split(',').ToList();

                //string userName = listAnswer[0];
                //string choice = listAnswer[1];
                string guess = listAnswer[2];

     
               
                if (guess == yesNumber.ToString()) //j state
                {
                    correctGuess++;
                }
            }

            for (int i = 0; i < Values.Instance.UserList.Count(); i++)
            {
                var listAnswer = ValuesSession.Instance.UserResponse[i].Split(',').ToList();

                string userName = listAnswer[0];
                //string choice = listAnswer[1];
                string guess = listAnswer[2];   

                if (guess == yesNumber.ToString() && match == true) //j state
                {
                    int j = 0;
                    
                    bool scoreUpdated = false;
                   
                    while (scoreUpdated == false)
                    {
                       
    
                            var scoreL = Values.Instance.ScoreList[j].Split(',').ToList();
                            string userInList = scoreL[0];
                            int scoreInListUser = int.Parse(scoreL[1]);
                        
                            
                            //var User = ValuesSession.Instance.UserResponse[j].Split(',').ToList();
                            //string userInList = User[0];
                            //int score = int.Parse(User[2]);

                            if (userInList == userName) //ACTUALISE
                            {
                                int newScore = scoreInListUser + 2;
                                Values.Instance.ScoreList.Remove(userName + "," + scoreInListUser);
                                Values.Instance.ScoreList.Add(userName + "," + newScore.ToString());
                                scoreUpdated = true;
                                ValuesSession.Instance.MarkPoint.Add(userName + "," + newScore.ToString() + "," + ValuesSession.Instance.Point);
                        }
                           
                        j++;
                    }       
                }

                
                if (guess == yesNumber.ToString() && match == false) //j state
                {
                    int j = 0;
                    

                
                    bool scoreUpdated = false;
                    ;
                    while (scoreUpdated == false)
                    {
                   

                        var scoreL = Values.Instance.ScoreList[j].Split(',').ToList();
                        string userInList = scoreL[0];
                        int scoreInListUser = int.Parse(scoreL[1]);

                        if (userInList == userName) //ACTUALISE
                        {
                            int newScore = scoreInListUser + 2;
                            Values.Instance.ScoreList.Remove(userName + "," + scoreInListUser);
                            Values.Instance.ScoreList.Add(userName + "," + newScore.ToString());
                            scoreUpdated = true;
                            ValuesSession.Instance.MarkPoint.Add(userName + "," + newScore.ToString() + "," + ValuesSession.Instance.Point);
                        }

                        j++;
                    }
                }
            }
                
                //if (guess != ValuesSession.Instance.RightString)
            

        

            for (int j = 0; j < Values.Instance.ScoreList.Count(); j++)
            {
                var User = Values.Instance.ScoreList[j].Split(',').ToList();
                string userInList = User[0];
                string score = User[1];
                Console.WriteLine(userInList + " : " + score);
            }
            ValuesSession.Instance.UserResponse.Clear();
            ValuesSession.Instance.Update = true;
        }


        public void InitiliazeQuiz()
        {
            //ValuesSession.Instance.QuizName = Values.Instance.File.Substring(0, Values.Instance.File.Length - 4);

            string line;

            using (StreamReader reader = new StreamReader(Values.Instance.Folder + "\\" + Values.Instance.File))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    var list = line.Split(',').ToList();
                    ValuesSession.Instance.QuestionList.Add(list[0]);
                }
            }

            for (int i = 0; i < Values.Instance.UserList.Count(); i++)
            {
                var userSession = Values.Instance.UserList[i].Split(',').ToList();

                string userName = userSession[0];

                Values.Instance.ScoreList.Add(userName + ",0");
                //string choice = listAnswer[1];
                Console.WriteLine("Score list member" + Values.Instance.ScoreList[i]);
            }
                //var rnd = new Random();
                //var randomized = ValuesSession.Instance.QuestionList.OrderBy(item => rnd.Next()); //need AnswerList sync


                ValuesSession.Instance.CurrentQuestion = ValuesSession.Instance.QuestionList[0];
        }


    }
}
