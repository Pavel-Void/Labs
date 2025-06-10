using System;

namespace Lab2_2_3
{
    public class Money
    {
        public uint Rubles { get; private set; }
        public byte Kopeks { get; private set; }

        public Money(uint rubles, byte kopeks)
        {
            if (kopeks >= 100)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(kopeks),
                    "Копейки должены быть от 0 до 99"
                );
            }

            Rubles = rubles;
            Kopeks = kopeks;
        }

        public Money AddKopeks(uint kopeksToAdd)
        {
            uint totalKopeks = (uint)Kopeks + kopeksToAdd;
            uint newRubles = Rubles + totalKopeks / 100;
            byte newKopeks = (byte)(totalKopeks % 100);

            return new Money(newRubles, newKopeks);
        }

        public static Money operator ++(Money money) => money.AddKopeks(1);

        public static Money operator --(Money money)
        {
            if (money.Rubles == 0 && money.Kopeks == 0)
                throw new InvalidOperationException("Невозможно уменьшить нулевую сумму");

            uint totalKopeks = money.Rubles * 100 + money.Kopeks - 1;
            return new Money(totalKopeks / 100, (byte)(totalKopeks % 100));
        }

        public static explicit operator uint(Money money) => money.Rubles;

        public static implicit operator double(Money money) => money.Kopeks / 100.0;

        public static Money operator +(Money money, uint kopeksToAdd) => money.AddKopeks(kopeksToAdd);

        public static Money operator +(uint kopeksToAdd, Money money) => money.AddKopeks(kopeksToAdd);

        public static Money operator -(Money money, uint kopeksToSubtract)
        {
            uint totalKopeks = money.Rubles * 100 + money.Kopeks;

            if (kopeksToSubtract > totalKopeks)
                throw new InvalidOperationException("Сумма для вычитания превышает текущее значение");

            totalKopeks -= kopeksToSubtract;
            return new Money(totalKopeks / 100, (byte)(totalKopeks % 100));
        }

        public static Money operator -(uint totalKopeks, Money money)
        {
            uint moneyTotalKopeks = money.Rubles * 100 + money.Kopeks;

            if (moneyTotalKopeks > totalKopeks)
                throw new InvalidOperationException("Сумма для вычитания превышает исходное значение");

            uint result = totalKopeks - moneyTotalKopeks;
            return new Money(result / 100, (byte)(result % 100));
        }

        public override string ToString() => $"{Rubles} руб. {Kopeks:D2} коп.";
    }
}