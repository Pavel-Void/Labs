using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    class Cat : IMeowing
    {
        string name;

        public Cat(string name)
        {
            this.name = name;
        }

        public void Meow()
        {
            Console.WriteLine($"{name}: meow!");
        }

        public void Meow(int n)
        {
            if (n < 1)
            {
                Console.WriteLine($"{name}: ...");
                return;
            }

            Console.Write($"{name}: ");
            for (int i = 0; i < n - 1; i++)
            {
                Console.Write("meow-");
            }
            Console.WriteLine("meow!");
        }

        public override string ToString()
        {
            return $"Cat: {name}";
        }
    }
}
