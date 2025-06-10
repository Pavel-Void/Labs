using System;

namespace LAB2
{
    class Program
    {
        static void Main()
        {
            try
            {
                TestBaseClass();
                TestDaughterClass();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void TestBaseClass()
        {
            Console.WriteLine("========Тест базового класса========");

            string text = ReadNonEmptyInput("Введите текст");
            var baseObj = new BaseClass(text);

            Console.WriteLine(baseObj.ToString());
            baseObj.AddExclamations();
            Console.WriteLine(baseObj.ToString());

            // Тест конструктора копирования
            var copiedBaseObj = new BaseClass(baseObj);
            Console.WriteLine(copiedBaseObj.ToString());
        }

        static void TestDaughterClass()
        {
            Console.WriteLine("========Тест дочернего класса========");

            string text1 = ReadNonEmptyInput("Введите первое слово или предложение");
            string text2 = ReadNonEmptyInput("Введите второе слово или предложение");

            var daughterObj = new DaughterClass(text1, text2);
            Console.WriteLine(daughterObj.ToString());

            Console.WriteLine($"Количество символов: {daughterObj.GetTextLength()}");
            Console.WriteLine($"Объединённый текст: {daughterObj.CombineText()}");

            daughterObj.AddExclamations();
            Console.WriteLine(daughterObj.ToString());
        }

        static string ReadNonEmptyInput(string prompt)
        {
            string input;
            do
            {
                Console.WriteLine(prompt);
                input = Console.ReadLine()?.Trim() ?? "";

                if (string.IsNullOrEmpty(input))
                    Console.WriteLine("Ошибка: ввод не может быть пустым. Повторите попытку.");

            } while (string.IsNullOrEmpty(input));

            return input;
        }
    }
}