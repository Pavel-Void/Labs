using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2_2_3
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.Write("Введите количество рублей:");
            uint rubles;
            while (!uint.TryParse(Console.ReadLine(), out rubles))
            {
                Console.Write("Пожалуйста, введите корректное целое число для рублей:");
            }

            Console.Write("Введите количество копеек (0–99):");
            byte kopeks;
            while (!byte.TryParse(Console.ReadLine(), out kopeks) || kopeks >= 100)
            {
                Console.Write("Пожалуйста, введите корректное значение для копеек (от 0 до 99):");
            }

            Money money = new Money(rubles, kopeks);
            Console.WriteLine("Начальная сумма: " + money);

            Console.Write("Введите количество копеек для добавления:");
            uint kopeksToAdd;
            while (!uint.TryParse(Console.ReadLine(), out kopeksToAdd))
            {
                Console.Write("Пожалуйста, введите корректное значение для копеек:");
            }

            money.AddKopeks(kopeksToAdd);
            Console.WriteLine("После добавления " + kopeksToAdd + " копеек: " + money);

            money++;
            Console.WriteLine("После увеличения на 1 копейку (оператор ++): " + money);

            money--;
            Console.WriteLine("После уменьшения на 1 копейку (оператор --): " + money);

            uint rublesOnly = (uint)money;
            Console.WriteLine("Рубли (явное приведение): " + rublesOnly);

            double fractionalRubles = money;
            Console.WriteLine("Дробные рубли (неявное приведение): " + fractionalRubles);

            Console.Write("Введите количество копеек для добавления (операция +):");
            uint addKopeks;
            while (!uint.TryParse(Console.ReadLine(), out addKopeks))
            {
                Console.Write("Пожалуйста, введите корректное значение для копеек:");
            }
            Money result1 = money + addKopeks;
            Console.WriteLine("После добавления " + addKopeks + " копеек (операция +): " + result1);

            Console.Write("Введите количество копеек для вычитания (операция -):");
            uint subtractKopeks;
            while (!uint.TryParse(Console.ReadLine(), out subtractKopeks))
            {
                Console.Write("Пожалуйста, введите корректное значение для копеек:");
            }
            Money result2 = money - subtractKopeks;
            Console.WriteLine("После вычитания " + subtractKopeks + " копеек (операция -): " + result2);
        }

    }
}
