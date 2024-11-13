using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB2
{
    class Program
    {
        static void Main()
        {   
            // Тест базового класса
            string text;
            
            Console.WriteLine("========Тест базового класса========");
            Console.WriteLine("Ввведите текст");

            text = Console.ReadLine();
            BaseClass baseObj = new BaseClass(text);
            Console.WriteLine(baseObj.ToString()); //копирует введённый текст

            baseObj.AddExclamations();
            Console.WriteLine(baseObj.ToString()); //!!! + введёный текст

            // Тест конструктора копирования
            BaseClass copiedBaseObj = new BaseClass(baseObj);
            Console.WriteLine(copiedBaseObj.ToString());

            // Тест дочернего класса
            string text1, text2;
            
            Console.WriteLine("=========Тест дочернего класса=========");
            Console.WriteLine("Ввведите первое слово или предложение");
            text1 = Console.ReadLine();

            Console.WriteLine("Ввведите второе слово или предложение");
            text2 = Console.ReadLine();
            DaughterClass DaughterObj = new DaughterClass(text1, text2);
            Console.WriteLine(DaughterObj.ToString());

            // Тест методов дочернего класса
            Console.WriteLine("Количество символов: " + DaughterObj.GetTextLength()); //длина строки text
            Console.WriteLine("Объеденённый текст: " + DaughterObj.CombineText());    

            // Тест метода AddExclamations() у дочернего класса
            DaughterObj.AddExclamations();
            Console.WriteLine(DaughterObj.ToString());
        }
    }
}