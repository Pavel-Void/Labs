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
            int taskChoice = int.Parse(Console.ReadLine());

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
                default:
                    Console.WriteLine("Неверный номер задания!");
                    break;
            }
        }

        private static void HandleTask1()
        {
            Console.WriteLine("Введите элементы списка L через пробел:");
            var L = Console.ReadLine().Split().Select(int.Parse).ToList();

            Console.WriteLine("Введите элементы списка L1 через пробел:");
            var L1 = Console.ReadLine().Split().Select(int.Parse).ToList();

            Console.WriteLine("Введите элементы списка L2 через пробел:");
            var L2 = Console.ReadLine().Split().Select(int.Parse).ToList();

            ReplaceSublistInList(L, L1, L2);

            Console.WriteLine("Результат списка L:");
            Console.WriteLine(string.Join(" ", L));
        }

        private static void HandleTask2()
        {
            Console.WriteLine("Введите элементы списка через пробел для сортировки по возрастанию:");
            var linkedList = new LinkedList<int>(Console.ReadLine().Split().Select(int.Parse));

            SortLinkedList(linkedList);

            Console.WriteLine("Отсортированный список:");
            Console.WriteLine(string.Join(" ", linkedList));
        }

        private static void HandleTask3()
        {
            Console.WriteLine("Введите перечень игр через запятую:");
            var games = new HashSet<string>(Console.ReadLine().Split(',').Select(s => s.Trim()));

            Console.WriteLine("Введите количество студентов:");
            int studentCount = int.Parse(Console.ReadLine());

            var studentGames = new List<HashSet<string>>();

            for (int i = 0; i < studentCount; i++)
            {
                Console.WriteLine($"Введите игры, в которые играет студент {i + 1}, через запятую:");
                studentGames.Add(new HashSet<string>(Console.ReadLine().Split(',').Select(s => s.Trim())));
            }

            AnalyzeGames(games, studentGames);
        }

        private static void HandleTask4()
        {
            Console.WriteLine("Введите путь к файлу");
            var filePath = Console.ReadLine();

            if (File.Exists(filePath))
            {
                PrintDeafConsonants(filePath);
            }
            else
            {
                Console.WriteLine("Файл не найден!");
            }
        }

        // Задание 1
        public static void ReplaceSublistInList(List<int> L, List<int> L1, List<int> L2)
        {
            int index = L.FindIndex(sublist => L.Skip(sublist).Take(L1.Count).SequenceEqual(L1));
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
            // Задаем глухие согласные
            char[] deafConsonants = { 'к', 'п', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш' };

            // Читаем текст из файла
            string text;
            try
            {
                text = File.ReadAllText(filePath).ToLower(); // Считываем текст и переводим в нижний регистр
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка чтения файла: {ex.Message}");
                return;
            }

            // Разбиваем текст на слова
            var words = text.Split(new[] { ' ', '\n', '\r', '\t', '.', ',', '!', '?', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);

            // Определяем буквы, которые не входят хотя бы в одно слово
            var missingLetters = new SortedSet<char>(deafConsonants);

            foreach (var word in words)
            {
                foreach (var letter in word)
                {
                    if (deafConsonants.Contains(letter))
                    {
                        missingLetters.Remove(letter); // Убираем буквы, которые встретились в текущем слове
                    }
                }
            }

            // Печатаем результат
            Console.WriteLine("Глухие согласные, которые отсутствуют хотя бы в одном слове:");
            Console.WriteLine(string.Join(", ", missingLetters));
        }
    }
}
