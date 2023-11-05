using BusinessLogic;
using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace ConsoleQuiz
{
    public class ConsoleStart
    {
        QuizGame quizGame = new QuizGame();
        UserModel userModel = new UserModel();
        public void startApp()
        {
            bool menuCycle = true;
            while (menuCycle)
            {
                int decision = Menu.menu("МЕГА ВIКТОРИНА", "Реєстрацiя", "Авторизацiя", "Вихiд");
                switch (decision)
                {
                    case 1:
                        {
                            try
                            {
                                Console.Clear();
                                string login, password, birthday;
                                Console.SetCursorPosition(Console.LargestWindowWidth / 7, Console.LargestWindowHeight / 5);
                                Console.Write("(*обов'язкове поле) Iм'я користувача ");
                                login = Console.ReadLine();
                                Console.SetCursorPosition(Console.LargestWindowWidth / 7, Console.LargestWindowHeight / 5 + 1);
                                Console.Write("(*обов'язкове поле) Пароль ");
                                password = Console.ReadLine();
                                Console.SetCursorPosition(Console.LargestWindowWidth / 7, Console.LargestWindowHeight / 5 + 2);
                                Console.Write("(*обов'язкове поле) Дата народження ");
                                birthday = Console.ReadLine();
                                try
                                {
                                    Console.Clear();
                                    userModel = quizGame.registration(login, password, birthday);
                                    Console.SetCursorPosition(Console.LargestWindowWidth / 7, Console.LargestWindowHeight / 5);
                                    Console.WriteLine("Реєстрацiя успiшна");
                                    Console.ReadKey();
                                    startGame();
                                }
                                catch (Exception ex) 
                                {
                                    Console.Clear();
                                    Console.SetCursorPosition(Console.LargestWindowWidth / 7, Console.LargestWindowHeight / 5);
                                    Console.WriteLine(ex.Message);
                                    Console.ReadKey();
                                }
                                break;
                            }
                            catch (Exception exc)
                            {
                                Console.Clear();
                                Console.SetCursorPosition(Console.LargestWindowWidth / 7, Console.LargestWindowHeight / 5);
                                Console.WriteLine(exc.Message);
                                Console.ReadKey();
                            }
                        }; break;
                    case 2:
                        {
                            try
                            {
                                Console.Clear();
                                string login, password;
                                Console.SetCursorPosition(Console.LargestWindowWidth / 7, Console.LargestWindowHeight / 5);
                                Console.Write("Iм'я користувача ");
                                login = Console.ReadLine();
                                Console.SetCursorPosition(Console.LargestWindowWidth / 7, Console.LargestWindowHeight / 5 + 1);
                                Console.Write("Пароль ");
                                password = Console.ReadLine();
                                userModel = quizGame.authorization(login, password);
                                if (login == "admin" && password == "admin")
                                    adminEntrance();
                                else
                                    startGame();
                                break;
                            }
                            catch (Exception exc)
                            {
                                Console.Clear();
                                Console.SetCursorPosition(Console.LargestWindowWidth / 7, Console.LargestWindowHeight / 5);
                                Console.WriteLine(exc.Message);
                                Console.ReadKey();
                            }

                        }; break;
                    case 3:
                        {
                            Console.Clear();
                            menuCycle = false;
                            break;
                        };
                }
            }
        }
        public void startGame()
        {
            bool menuCycle = true;
            while (menuCycle)
            {
                Console.Clear();
                int choice = Menu.menu($"Вiтаємо у ГОЛОВНОМУ МЕНЮ, {userModel.Login}", "Нова вiкторина", "Результати iгор", "Тор 20", "Налаштування", "Вихiд");
                switch (choice)
                {
                    case 1: 
                        newQuiz(); 
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine(quizGame.getScores(userModel));
                        Console.ReadKey(); 
                        break;
                    case 3:
                        Console.Clear();
                        int chapter = Menu.menu("ОБЕРIТЬ РОЗДIЛ", quizGame.getQuizNameCatalogue());
                        Dictionary<string, int> top20 = quizGame.getTop20(quizGame.getQuizNameCatalogue(chapter));
                        Console.Clear();
                        foreach (KeyValuePair<string, int> item in top20)
                        {
                            Console.WriteLine($"{item.Key} - {item.Value}");
                        }
                        Console.ReadKey(); 
                        break;
                    case 4:
                        Console.Clear();
                        string password, birthday;
                        Console.SetCursorPosition(Console.LargestWindowWidth / 7, Console.LargestWindowHeight / 5);
                        Console.Write("Новий пароль ");
                        password = Console.ReadLine();
                        Console.SetCursorPosition(Console.LargestWindowWidth / 7, Console.LargestWindowHeight / 5 + 1);
                        Console.Write("Нова дата народження ");
                        birthday = Console.ReadLine();
                        if (quizGame.settings(userModel,password, birthday))
                        {
                            Console.Clear();
                            Console.SetCursorPosition(Console.LargestWindowWidth / 7, Console.LargestWindowHeight / 5);
                            Console.WriteLine("Змiни збереженi");
                        }
                        else
                        {
                            Console.Clear();
                            Console.SetCursorPosition(Console.LargestWindowWidth / 7, Console.LargestWindowHeight / 5);
                            Console.WriteLine("Змiни не збереженi");
                        }
                        quizGame.uploadUsersData();
                        Console.ReadKey(); 
                        break;
                    case 5: 
                        quizGame.uploadUsersData();
                        menuCycle = false;
                        break;
                }
            }
        }
        public void newQuiz()
        {
            try
            {
                Console.Clear();
                int choice = Menu.menu("ОБЕРIТЬ РОЗДIЛ", quizGame.getQuizNameCatalogue());
                string choicetext = quizGame.getQuizNameCatalogue(choice);                
                Console.Clear();
                Console.SetCursorPosition(Console.LargestWindowWidth / 7, Console.LargestWindowHeight / 5);
                Console.WriteLine("Ви матимете лише один шанс для набрання балiв.");
                Console.ReadKey();
                if (choicetext == "Все в одному мiкс")
                {
                    Random rand = new Random();
                    List<int> randomed = new List<int>();
                    quizGame.getQuizNameCatalogue().RemoveAt(0);
                    quizGame.getQuizNameCatalogue().RemoveAt(quizGame.getQuizNameCatalogue().Count-1);
                    quizGame.downloadQuiz(quizGame.getQuizNameCatalogue().ToArray());
                    for (int i = 0; i < 20; i++)
                    {
                        bool flag = true;
                        int rd = rand.Next(1, rand.Next(1, quizGame.getQuestion().Count + 1));
                        while (flag)
                        {
                            if (!randomed.Contains(rd))
                            {
                                randomed.Add(rd);
                                flag = false;
                            }
                            else
                            {
                                rd = rand.Next(1, rand.Next(1, quizGame.getQuestion().Count + 1));
                            }
                        }
                        quizGame.getQuestion()[rd].UsersAnswer = Menu.menu(quizGame.getQuestion()[rd].Question, quizGame.getQuestion()[rd].Variant1, quizGame.getQuestion()[rd].Variant2, quizGame.getQuestion()[rd].Variant3, quizGame.getQuestion()[rd].Variant4);
                        rightWrongAnswer(quizGame.getQuestion()[rd].CorrectAnswer, quizGame.getQuestion()[rd].UsersAnswer, quizGame.getQuestion()[rd].Question, quizGame.getQuestion()[rd].Variant1, quizGame.getQuestion()[rd].Variant2, quizGame.getQuestion()[rd].Variant3, quizGame.getQuestion()[rd].Variant4);
                        Console.ReadKey();
                    }
                }
                else
                {
                    quizGame.downloadQuiz(choicetext);
                    for (int i = 0; i < quizGame.getQuestion().Count(); i++)
                    {
                        quizGame.getQuestion()[i].UsersAnswer = Menu.menu(quizGame.getQuestion()[i].Question, quizGame.getQuestion()[i].Variant1, quizGame.getQuestion()[i].Variant2, quizGame.getQuestion()[i].Variant3, quizGame.getQuestion()[i].Variant4);
                        rightWrongAnswer(quizGame.getQuestion()[i].CorrectAnswer, quizGame.getQuestion()[i].UsersAnswer, quizGame.getQuestion()[i].Question, quizGame.getQuestion()[i].Variant1, quizGame.getQuestion()[i].Variant2, quizGame.getQuestion()[i].Variant3, quizGame.getQuestion()[i].Variant4);
                        Console.ReadKey();
                    }
                }
                Console.Clear();
                Console.SetCursorPosition(Console.LargestWindowWidth / 7, Console.LargestWindowHeight / 5);
                int sumRezult = quizGame.sumUpResults();
                quizGame.addResults(userModel, choicetext, sumRezult);
                quizGame.uploadUsersData();
                Console.WriteLine($"{sumRezult} правильних вiдповiдей з 20");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "IНФОРМАЦІЯ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void rightWrongAnswer(int right, int wrong, params string[] menuItems)
        {
            Console.Clear();
            Console.CursorVisible = false;
            List<string> menu = new List<string>();
            for (int i = 0; i < menuItems.Length; i++)
            {
                if (!string.IsNullOrEmpty(menuItems[i]))
                    menu.Add(menuItems[i]);
            }
            for (int i = 0; i < menu.Count; i++)
            {
                Console.SetCursorPosition(Console.LargestWindowWidth / 7, Console.LargestWindowHeight / 5 + i);
                if (i == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(menu[i]);
                }
                else if (i == right)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(menu[i]);
                    Console.ResetColor();
                }
                else if(i == wrong) 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(menu[i]);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(menu[i]);
                }
            }            
        }
        public void adminEntrance()
        {
            bool menuCycle = true;
            while (menuCycle)
            {
                Console.Clear();
                int decision = Menu.menu($"Вiтаємо у ГОЛОВНОМУ МЕНЮ, {userModel.Login}", "Створити нову вiкторину", "Редагувати вiкторину", "Вихiд");
                switch (decision)
                {
                    case 1:
                        Console.Clear();
                        Console.SetCursorPosition(Console.LargestWindowWidth / 7, Console.LargestWindowHeight / 5);
                        string qname;
                        quizGame.getQuestion().Clear();
                        Console.Write("Назва вiкторини: ");
                        qname = Console.ReadLine();
                        bool qcycle = true;
                        while (qcycle)
                        {
                            Console.Clear();
                            string question, option1, option2, option3, option4;
                            int correct = 0;
                            Console.SetCursorPosition(Console.LargestWindowWidth / 7, Console.LargestWindowHeight / 5);
                            Console.Write("Питання: ");
                            question = Console.ReadLine();
                            Console.SetCursorPosition(Console.LargestWindowWidth / 7, Console.LargestWindowHeight / 5 + 1);
                            Console.Write("Варіант вiдповiдi 1: ");
                            option1 = Console.ReadLine();
                            Console.SetCursorPosition(Console.LargestWindowWidth / 7, Console.LargestWindowHeight / 5 + 2);
                            Console.Write("Варіант вiдповiдi 2: ");
                            option2 = Console.ReadLine();
                            Console.SetCursorPosition(Console.LargestWindowWidth / 7, Console.LargestWindowHeight / 5 + 3);
                            Console.Write("Варіант вiдповiдi 3: ");
                            option3 = Console.ReadLine();
                            Console.SetCursorPosition(Console.LargestWindowWidth / 7, Console.LargestWindowHeight / 5 + 4);
                            Console.Write("Варіант вiдповiдi 4: ");
                            option4 = Console.ReadLine();
                            Console.SetCursorPosition(Console.LargestWindowWidth / 7, Console.LargestWindowHeight / 5 + 5);
                            Console.Write("Правильний варіант вiдповiдi(номер): ");
                            correct = Convert.ToInt32(Console.ReadLine());
                            quizGame.createQuiz(question, option1, option2, option3, option4, correct);
                            int answer = Menu.menu("Додати ще одне питання?", "Так", "Нi");
                            switch (answer)
                            {
                                case 1: continue;
                                case 2:
                                    quizGame.getQuizNameCatalogue().Add(qname);
                                    quizGame.uploadQuiz(qname, quizGame.getQuestion()); 
                                    quizGame.uploadQuizNameCatalogue();
                                    qcycle = false; 
                                    break;
                            }
                        }; break;
                    case 2:
                        Console.Clear();
                        int choice = Menu.menu("ОБЕРIТЬ РОЗДIЛ", quizGame.getQuizNameCatalogue());
                        string choicetext = quizGame.getQuizNameCatalogue(choice);
                        List<QuestionModel> toedit = quizGame.editQuiz(choicetext);
                        for (int i = 0; i < toedit.Count(); i++)
                        {
                            Console.Clear();
                            Console.WriteLine(toedit[i].Question);
                            Console.WriteLine(toedit[i].Variant1);
                            Console.WriteLine(toedit[i].Variant2);
                            Console.WriteLine(toedit[i].Variant3);
                            Console.WriteLine(toedit[i].Variant4);
                            Console.WriteLine("Редагувати це питання - натиснiть 1 \nНаступне питання - натиснiть 2");
                            int option = Convert.ToInt32(Console.ReadLine());
                            switch (option)
                            {
                                case 1:
                                    Console.Write("Editting question: ");
                                    toedit[i].Question = Console.ReadLine();
                                    Console.Write("Editting option 1: ");
                                    toedit[i].Variant1 = Console.ReadLine();
                                    Console.Write("Editting option 2: ");
                                    toedit[i].Variant2 = Console.ReadLine();
                                    Console.Write("Editting option 3: ");
                                    toedit[i].Variant3 = Console.ReadLine();
                                    Console.Write("Editting option 4: ");
                                    toedit[i].Variant4 = Console.ReadLine();
                                    Console.Write("Correct answer(number): ");
                                    toedit[i].CorrectAnswer = Convert.ToInt32(Console.ReadLine()); break;
                                case 2:
                                    continue;
                            }
                        }
                        bool flag = true;
                        while(flag)
                        {
                            Console.Clear();
                            Console.WriteLine("Додати питання - натиснiть 1 \nЗавершити виправлення - натиснiть 2");
                            int opt = Convert.ToInt32(Console.ReadLine());
                            switch (opt)
                            {
                                case 1:
                                    QuestionModel newQuest = new QuestionModel();
                                    Console.Write("New question: ");
                                    newQuest.Question = Console.ReadLine();
                                    Console.Write(" option 1: ");
                                    newQuest.Variant1 = Console.ReadLine();
                                    Console.Write(" option 2: ");
                                    newQuest.Variant2 = Console.ReadLine();
                                    Console.Write(" option 3: ");
                                    newQuest.Variant3 = Console.ReadLine();
                                    Console.Write(" option 4: ");
                                    newQuest.Variant4 = Console.ReadLine();
                                    Console.Write("Correct answer(number): ");
                                    newQuest.CorrectAnswer = Convert.ToInt32(Console.ReadLine());
                                    toedit.Add(newQuest);
                                    break;
                                case 2:
                                    quizGame.uploadQuiz(choicetext, toedit);
                                    flag = false;
                                    break;
                            } 
                        }; break;
                    case 3:
                        menuCycle = false;
                        break;
                }
            }
        }
    }
}

