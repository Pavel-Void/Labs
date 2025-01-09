using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    class Fraction : ICloneable, IFraction
    {
        int num;
        int denom;

        public Fraction(int num, int denom)
        {
            if (denom == 0)
            {
                Console.WriteLine("Denomerator can't be zero!!");
                return;
            }

            if (denom < 0)
            {
                num *= -1;
                denom *= -1;
            }

            this.num = num;
            this.denom = denom;

            Simple();
        }

        public Fraction(int num)
        {
            this.num = num;
            this.denom = 1;
        }

        public int GetNum()
        {
            return num;
        }

        public int GetDenom()
        {
            return denom;
        }

        static int NOD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return Math.Abs(a);
        }

        void Simple()
        {
            int nod = NOD(num, denom);
            num /= nod;
            denom /= nod;

            if (denom < 0)
            {
                num *= -1;
                denom *= -1;
            }
        }

        public override string ToString()
        {
            if (denom == 1) return $"{num}";
            return $"{num}/{denom}";
        }

        public static Fraction operator + (Fraction a, Fraction b)
        {
            return new Fraction(a.GetNum() * b.GetDenom() + a.GetDenom() * b.GetNum(), a.GetDenom() * b.GetDenom());
        }

        public static Fraction operator - (Fraction a, Fraction b)
        {
            return new Fraction(a.GetNum() * b.GetDenom() - a.GetDenom() * b.GetNum(), a.GetDenom() * b.GetDenom());
        }

        public static Fraction operator * (Fraction a, Fraction b)
        {
            return new Fraction(a.GetNum() * b.GetNum(), a.GetDenom() * b.GetDenom());
        }

        public static Fraction operator / (Fraction a, Fraction b)
        {
            return new Fraction(a.GetNum() * b.GetDenom(), a.GetDenom() * b.GetNum());
        }

        public static implicit operator Fraction(int a)
        {
            return new Fraction(a);
        }

        public static bool operator == (Fraction a, Fraction b)
        {
            return a.GetNum() == b.GetNum() && a.GetDenom() == b.GetDenom();
        }

        public static bool operator != (Fraction a, Fraction b)
        {
            return a.GetNum() != b.GetNum() || a.GetDenom() != b.GetDenom();
        }

        public object Clone()
        {
            return new Fraction(num, denom);
        }

        public double DoubleVal()
        {
            return Convert.ToDouble(num) / Convert.ToDouble(denom);
        }
    }
}
