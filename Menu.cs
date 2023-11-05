using System;
using System.Collections.Generic;

namespace ConsoleQuiz
{
    public static class Menu
    {
        public static int menu(params string[] menuItems)
        {
            while (true)
            {
                Console.Clear();
                Console.CursorVisible = false;
                int highlighted = 1;
                List<string> menu = new List<string>();
                for (int i = 0; i < menuItems.Length; i++)
                {
                    if (!string.IsNullOrEmpty(menuItems[i]))
                        menu.Add(menuItems[i]);
                }
                Console.SetWindowSize(Console.LargestWindowWidth / 2, Console.LargestWindowHeight / 2);
                Console.CursorVisible = false;
                int decision = 0;
                while (true)
                {
                    for (int i = 0; i < menu.Count; i++)
                    {
                        Console.SetCursorPosition(Console.LargestWindowWidth / 7, Console.LargestWindowHeight / 5 + i);
                        if (i == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine(menu[i]);
                        }
                        else if (i == highlighted)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine(menu[i]);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine(menu[i]);
                        }
                    }
                    ConsoleKey choice = Console.ReadKey().Key;
                    if (choice == ConsoleKey.DownArrow)
                    {
                        if (highlighted >= menu.Count - 1)
                            highlighted = 1;
                        else
                            highlighted += 1;
                    }
                    if (choice == ConsoleKey.UpArrow)
                    {
                        if (highlighted <= 1)
                            highlighted = menu.Count - 1;
                        else
                            highlighted -= 1;
                    }
                    if (choice == ConsoleKey.Enter)
                    {
                        decision = highlighted;
                        Console.ResetColor();
                        return decision;
                    }
                }
            }
        }
        public static int menu(string chapter, List<string> menu)
        {
            while (true)
            {
                Console.Clear();
                Console.CursorVisible = false;
                if (menu[0] != chapter)
                    menu.Insert(0, chapter);
                int highlighted = 1;
                Console.SetWindowSize(Console.LargestWindowWidth / 2, Console.LargestWindowHeight / 2);
                Console.CursorVisible = false;
                int decision = 0;
                while (true)
                {
                    for (int i = 0; i < menu.Count; i++)
                    {
                        Console.SetCursorPosition(Console.LargestWindowWidth / 7, Console.LargestWindowHeight / 5 + i);
                        if (i == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine(menu[0]);
                        }
                        else if (i == highlighted)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine(menu[i]);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine(menu[i]);
                        }
                    }
                    ConsoleKey choice = Console.ReadKey().Key;
                    if (choice == ConsoleKey.DownArrow)
                    {
                        if (highlighted >= menu.Count - 1)
                            highlighted = 1;
                        else
                            highlighted += 1;
                    }
                    if (choice == ConsoleKey.UpArrow)
                    {
                        if (highlighted <= 1)
                            highlighted = menu.Count - 1;
                        else
                            highlighted -= 1;
                    }
                    if (choice == ConsoleKey.Enter)
                    {
                        decision = highlighted;
                        Console.ResetColor();
                        return decision;
                    }
                }
            }
        }
    }
}

