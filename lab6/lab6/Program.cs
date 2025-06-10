using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            //1.1
            Console.WriteLine("Task 1.1:");
            Cat barsik = new Cat("Барсик");
            barsik.Meow();
            barsik.Meow(3);
            Console.WriteLine();

            //1.2
            Console.WriteLine("Task 1.2");
            Cat nastya = new Cat("Настя");
            AnotherCat fakecat = new AnotherCat("ASUS");
            HandlerMeow.MakeThemMeow(new List<IMeowing> { barsik, nastya, fakecat });
            Console.WriteLine();

            //1.3
            Console.WriteLine("Task 1.3:");
            var barsikMeows = new MeowTracker(barsik);
            barsikMeows.Meow(4);
            Console.WriteLine($"Барсик meowed {barsikMeows.GetMeowCount()} times.\n");



            //2.1
            Console.WriteLine("Task 2.1:");
            Fraction f1 = new Fraction(2, 4), f2 = new Fraction(3, 4), f3 = new Fraction(3, 2);
            Console.WriteLine($"{f1} + {f2} = {f1 + f2}");
            Console.WriteLine($"{f1} - {f2} = {f1 - f2}");
            Console.WriteLine($"{f1} * {f2} = {f1 * f2}");
            Console.WriteLine($"{f1} / {f2} = {f1 / f2}");
            Console.WriteLine($"{f1} + {f2}:{f3} - 5 = {f1 + f2/f3 - 5}\n");

            //2.2
            Console.WriteLine("Task 2.2:");
            Console.WriteLine($"{f1} == {f2}  {f1 == f2}");
            Console.WriteLine($"{f1} == {f2}:{f3}  {f1 == f2/f3}\n");

            //2.3
            Console.WriteLine("Task 2.3");
            Console.WriteLine($"{f1} clone = {f1.Clone()}\n");

            //2.4
            Console.WriteLine("Task 2.4:");
            IFraction f5 = new Fraction(4, 5);
            Console.WriteLine($"{f5} = {f5.DoubleVal()}\n");
        }
    }
}
