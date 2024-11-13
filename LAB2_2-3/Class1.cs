using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2_2_3
{
    public class Money
    {
        public uint Rubles { get; private set; }
        public byte Kopeks { get; private set; }

        public Money(uint rubles, byte kopeks)
        {
            Rubles = rubles;
            Kopeks = kopeks;
        }

        // Метод добавления произвольного количества копеек
        public Money AddKopeks(uint kopeksToAdd)
        {
            uint totalKopeks = (uint)Kopeks + kopeksToAdd;
            Rubles += totalKopeks / 100;
            Kopeks = (byte)(totalKopeks % 100);
            return this;
        }

        // Перегрузка унарного оператора++ для добавления одной копейки
        public static Money operator++(Money m)
        {
            return m.AddKopeks(1);
        }

        // Перегрузка унарного оператора-- для вычитания одной копейки
        public static Money operator--(Money m)
        {
            if (m.Kopeks == 0)
            {
                if (m.Rubles > 0)
                {
                    m.Rubles--;
                    m.Kopeks = 99;
                }
            }
            else
            {
                m.Kopeks--;
            }
            return m;
        }

        // Операция приведения к uint (явное приведение) - только рубли
        public static explicit operator uint(Money m)
        {
            return m.Rubles;
        }

        // Операция приведения к double (неявное приведение) - только дробная часть рубля
        public static implicit operator double(Money m)
        {
            return m.Kopeks / 100.0;
        }

        // Бинарная операция+ для Money и uint
        public static Money operator+(Money m, uint kopeksToAdd)
        {
            return new Money(m.Rubles, m.Kopeks).AddKopeks(kopeksToAdd);
        }

        public static Money operator+(uint kopeksToAdd, Money m)
        {
            return m + kopeksToAdd;
        }

        // Бинарная операция- для Money и uint
        public static Money operator-(Money m, uint kopeksToSubtract)
        {
            uint totalKopeks = m.Rubles * 100 + m.Kopeks;
            if (kopeksToSubtract > totalKopeks)
            {
                throw new InvalidOperationException("Невозможно вычесть больше, чем имеется монет.");
            }
            totalKopeks -= kopeksToSubtract;
            return new Money(totalKopeks / 100, (byte)(totalKopeks % 100));
        }

        public static Money operator -(uint kopeksToSubtract, Money m)
        {
            return m - kopeksToSubtract;
        }

        public override string ToString()
        {
            return $"{Rubles} руб. {Kopeks:D2} коп.";
        }
    }
}
