using System;

namespace Lab2_2_3
{
    internal class Program
    {
        static void Main()
        {
            try
            {
                uint rubles = ReadUIntInput("Введите количество рублей: ");
                byte kopeks = ReadByteInput("Введите количество копеек (0-99): ", 0, 99);

                Money money = new Money(rubles, kopeks);
                Console.WriteLine($"Начальная сумма: {money}");

                uint kopeksToAdd = ReadUIntInput("Введите количество копеек для добавления: ");
                money = money.AddKopeks(kopeksToAdd);
                Console.WriteLine($"После добавления {kopeksToAdd} копеек: {money}");

                money++;
                Console.WriteLine($"После увеличения на 1 копейку (оператор ++): {money}");

                money--;
                Console.WriteLine($"После уменьшения на 1 копейку (оператор --): {money}");

                uint rublesOnly = (uint)money;
                Console.WriteLine($"Рубли (явное приведение): {rublesOnly}");

                double fractionalPart = money;
                Console.WriteLine($"Дробные рубли (неявное приведение): {fractionalPart:F2}");

                // Тестирование бинарного + (обе стороны)
                uint addKopeks = ReadUIntInput("Введите количество копеек для добавления (оператор +): ");
                Money result1 = money + addKopeks;
                Money result2 = addKopeks + money;
                Console.WriteLine($"Результат money + kopeks: {result1}");
                Console.WriteLine($"Результат kopeks + money: {result2}");

                // Тестирование правостороннего вычитания
                uint subtractKopeks = ReadUIntInput("Введите количество копеек для вычитания (оператор -): ");
                Money result3 = money - subtractKopeks;
                Console.WriteLine($"Результат money - kopeks: {result3}");

                // Тестирование левостороннего вычитания
                uint totalKopeks = ReadUIntInput("Введите общее количество копеек для левостороннего вычитания: ");
                Money result4 = totalKopeks - money;
                Console.WriteLine($"Результат totalKopeks - money: {result4}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static uint ReadUIntInput(string prompt)
        {
            uint value;
            Console.Write(prompt);
            while (!uint.TryParse(Console.ReadLine(), out value))
            {
                Console.Write("Ошибка ввода. Пожалуйста, введите неотрицательное целое число: ");
            }
            return value;
        }

        static byte ReadByteInput(string prompt, byte min, byte max)
        {
            byte value;
            Console.Write(prompt);
            while (!byte.TryParse(Console.ReadLine(), out value) || value < min || value > max)
            {
                Console.Write($"Ошибка ввода. Пожалуйста, введите число от {min} до {max}: ");
            }
            return value;
        }
    }
}