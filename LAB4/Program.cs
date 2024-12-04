using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Tasks
{
    public static class TaskSolver
    {
        public static void Main()
        {
            Console.WriteLine("Выберите задание (1-4):");
            if (!int.TryParse(Console.ReadLine(), out int taskChoice) || taskChoice < 1 || taskChoice > 4)
            {
                Console.WriteLine("Неверный ввод! Введите число от 1 до 4.");
                return;
            }

            switch (taskChoice)
            {
                case 1:
                    HandleTask1();
                    break;
                case 2:
                    HandleTask2();
                    break;
                case 3:
                    HandleTask3();
                    break;
                case 4:
                    HandleTask4();
                    break;
            }
        }

        private static void HandleTask1()
        {
            Console.WriteLine("Введите элементы списка L через пробел:");
            if (!TryParseIntList(Console.ReadLine(), out var L))
            {
                Console.WriteLine("Некорректный ввод списка L.");
                return;
            }

            Console.WriteLine("Введите элементы списка L1 через пробел:");
            if (!TryParseIntList(Console.ReadLine(), out var L1))
            {
                Console.WriteLine("Некорректный ввод списка L1.");
                return;
            }

            Console.WriteLine("Введите элементы списка L2 через пробел:");
            if (!TryParseIntList(Console.ReadLine(), out var L2))
            {
                Console.WriteLine("Некорректный ввод списка L2.");
                return;
            }

            ReplaceSublistInList(L, L1, L2);

            Console.WriteLine("Результат списка L:");
            Console.WriteLine(string.Join(" ", L));
        }

        private static void HandleTask2()
        {
            Console.WriteLine("Введите элементы списка через пробел для сортировки по возрастанию:");
            if (!TryParseIntList(Console.ReadLine(), out var list))
            {
                Console.WriteLine("Некорректный ввод списка.");
                return;
            }

            var linkedList = new LinkedList<int>(list);

            SortLinkedList(linkedList);

            Console.WriteLine("Отсортированный список:");
            Console.WriteLine(string.Join(" ", linkedList));
        }

        private static void HandleTask3()
        {
            Console.WriteLine("Введите перечень игр через запятую:");
            var gamesInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(gamesInput))
            {
                Console.WriteLine("Некорректный ввод перечня игр.");
                return;
            }

            var games = new HashSet<string>(gamesInput.Split(',').Select(s => s.Trim()));

            Console.WriteLine("Введите количество студентов:");
            if (!int.TryParse(Console.ReadLine(), out int studentCount) || studentCount <= 0)
            {
                Console.WriteLine("Некорректное количество студентов.");
                return;
            }

            var studentGames = new List<HashSet<string>>();

            for (int i = 0; i < studentCount; i++)
            {
                Console.WriteLine($"Введите игры, в которые играет студент {i + 1}, через запятую:");
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Некорректный ввод игр.");
                    return;
                }

                studentGames.Add(new HashSet<string>(input.Split(',').Select(s => s.Trim())));
            }

            AnalyzeGames(games, studentGames);
        }

        private static void HandleTask4()
        {
            string filePath = @"C:\Users\Pavel\repos\Labs\LAB4\Task4.txt";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл не найден!");
                return;
            }

            PrintDeafConsonants(filePath);
        }
        private static bool TryParseIntList(string input, out List<int> result)
        {
            result = null;

            if (string.IsNullOrWhiteSpace(input))
                return false;

            var elements = input.Split();
            if (elements.All(e => int.TryParse(e, out _)))
            {
                result = elements.Select(int.Parse).ToList();
                return true;
            }

            return false;
        }

        // Задание 1
        public static void ReplaceSublistInList(List<int> L, List<int> L1, List<int> L2)
        {
            int index = -1;
            for (int i = 0; i <= L.Count - L1.Count; i++)
            {
                if (L.Skip(i).Take(L1.Count).SequenceEqual(L1))
                {
                    index = i;
                    break;
                }
            }

            if (index != -1)
            {
                L.RemoveRange(index, L1.Count);
                L.InsertRange(index, L2);
            }
        }

        // Задание 2
        public static void SortLinkedList(LinkedList<int> linkedList)
        {
            var sorted = linkedList.OrderBy(x => x).ToList();
            linkedList.Clear();
            foreach (var item in sorted)
            {
                linkedList.AddLast(item);
            }
        }

        // Задание 3
        public static void AnalyzeGames(HashSet<string> games, List<HashSet<string>> studentGames)
        {
            var allPlay = new HashSet<string>(games);
            foreach (var set in studentGames)
                allPlay.IntersectWith(set);

            var somePlay = new HashSet<string>(studentGames.SelectMany(g => g));
            var nonePlay = new HashSet<string>(games);
            nonePlay.ExceptWith(somePlay);

            Console.WriteLine("Игры, в которые играют все студенты: " + string.Join(", ", allPlay));
            Console.WriteLine("Игры, в которые играют некоторые студенты: " + string.Join(", ", somePlay));
            Console.WriteLine("Игры, в которые не играет никто: " + string.Join(", ", nonePlay));
        }

        // Задание 4
        public static void PrintDeafConsonants(string filePath)
        {
            string text = File.ReadAllText(filePath);

            HashSet<char> deafConsonants = new HashSet<char>("пфктшсхцч");
            string[] words = text.ToLower().Split(new char[] { ' ', ',', '.', '!', '?', ';', ':', '-', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            //Создаем копию множества для работы
            HashSet<char> unusedDeafConsonants = new HashSet<char>(deafConsonants);

            foreach (string word in words)
            {
                foreach (char c in word)
                {
                    if (deafConsonants.Contains(c))
                    {
                        unusedDeafConsonants.Remove(c);
                    }
                }
            }

            List<char> result = unusedDeafConsonants.ToList();
            result.Sort();

            Console.WriteLine("Глухие согласные, не входящие хотя бы в одно слово:");
            Console.WriteLine(string.Join(", ", result));
        }

    }
}
