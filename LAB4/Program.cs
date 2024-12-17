using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            Console.WriteLine("Введите элементы исходного списка L через пробел:");
            var L = ReadListFromConsole();

            Console.WriteLine("Введите элементы подсписка L1 через пробел:");
            var L1 = ReadListFromConsole();

            Console.WriteLine("Введите элементы списка L2 для замены через пробел:");
            var L2 = ReadListFromConsole();

            Console.WriteLine("Исходный список L:");
            PrintList(L);

            ReplaceSublist(L, L1, L2);

            Console.WriteLine("Изменённый список L:");
            PrintList(L);
        }

        private static void HandleTask2()
        {
            Console.WriteLine("Введите элементы списка через запятую:");
            var inputList = ReadListFromConsole();

            Console.WriteLine("Исходный список:");
            PrintList(inputList);

            try
            {
                // Попробовать отсортировать список
                var sortedList = SortList(inputList);

                Console.WriteLine("Отсортированный список:");
                PrintList(sortedList);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
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
            string filePath = @"C:\Users\Pavel\repos\Labs\LAB4\Task4.txt";

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
        public static void ReplaceSublist<T>(List<T> L, List<T> L1, List<T> L2)
        {
            // Найти первое вхождение подсписка L1 в L
            int index = FindSublist(L, L1);

            if (index != -1)
            {
                // Удалить элементы L1 из L
                L.RemoveRange(index, L1.Count);

                // Вставить элементы L2 в место удалённых
                L.InsertRange(index, L2);
            }
            else
            {
                Console.WriteLine("Подсписок L1 не найден в списке L.");
            }
        }

        public static int FindSublist<T>(List<T> L, List<T> L1)
        {
            for (int i = 0; i <= L.Count - L1.Count; i++)
            {
                bool match = true;
                for (int j = 0; j < L1.Count; j++)
                {
                    if (!EqualityComparer<T>.Default.Equals(L[i + j], L1[j]))
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                    return i;
            }
            return -1; // Если L1 не найден в L
        }

        public static List<string> ReadListFromConsole()
        {
            string input = Console.ReadLine();
            return input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(item => item.Trim()).ToList();
        }

        public static void PrintList<T>(List<T> list)
        {
            Console.WriteLine(string.Join(", ", list));
        }

        // Задание 2
        public static List<object> SortList(List<string> input)
        {
            // Определяем типы данных
            var parsedList = new List<object>();

            foreach (var item in input)
            {
                if (double.TryParse(item, out var number)) // Попытка преобразования в число
                    parsedList.Add(number);
                else
                    parsedList.Add(item); // Сохраняем как строку
            }

            // Проверяем, что все элементы одного типа
            var types = parsedList.Select(x => x.GetType()).Distinct().ToList();
            if (types.Count > 1)
                throw new InvalidOperationException("Список содержит элементы разных типов, сортировка невозможна.");

            // Сортируем элементы
            return parsedList.OrderBy(x => x).ToList();
        }

        // Задание 3
        public static void AnalyzeGames(HashSet<string> games, List<HashSet<string>> studentGames)
        {
            var allPlay = new HashSet<string>(games);
            foreach (var set in studentGames)
            {
                allPlay.IntersectWith(set); // Пересекаем множество игр со списком игр текущего студента
            }

            var somePlay = new HashSet<string>();
            foreach (var set in studentGames)
            {
                foreach (var game in set)
                {
                    somePlay.Add(game); 
                }
            }

            // Игры, в которые не играет никто
            var nonePlay = new HashSet<string>(games);
            foreach (var game in somePlay)
            {
                nonePlay.Remove(game);
            }

            // Вывод результатов
            Console.WriteLine("Игры, в которые играют все студенты: " + string.Join(", ", allPlay));
            Console.WriteLine("Игры, в которые играют некоторые студенты: " + string.Join(", ", somePlay));
            Console.WriteLine("Игры, в которые не играет никто: " + string.Join(", ", nonePlay));
        }


        // Задание 4
        public static void PrintDeafConsonants(string filePath)
        {
            // Задаем глухие согласные
            var deafConsonants = new HashSet<char> { 'к', 'п', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш' };

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

            foreach (char word in text)
            {
                if (deafConsonants.Contains(word))
                {
                    deafConsonants.Remove(word); // Убираем буквы, которые встретились в текущем слове
                }
            }

            if (deafConsonants.Count > 0)
            {
                Console.WriteLine("Глухие согласные буквы, которые не входят хотя бы в одно слово:");
                Console.WriteLine(string.Join(", ", deafConsonants));
            }
            else
            {
                Console.WriteLine("Все глухие слогласные буквы встречаються в тексте");
            }
        }
    }
}