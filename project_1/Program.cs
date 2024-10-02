using System.Linq;
using System;

class Taa
{
    public static void Main(String[] args)
    {
        Taa taa = new Taa();
        int task = -1;
        Console.WriteLine("Выберете задачу: ");
        task = enterNum(1, 20);
        int n = 0;
        switch (task)
        {
            //1.2
            case 1:
                n = enterNumNotIncludingGap(-10, 10);
                int sum = taa.sumLastNums(n);
                Console.WriteLine("Сумма последних двух цифр числа " + n + " = " + sum);
                break;

            case 2:
                //1.4
                n = enterNum();
                if (taa.isPositive(n)) Console.WriteLine("Число " + n + " - положительное.");
                else Console.WriteLine("Число " + n + " - не положительное");
                break;
            case 3:
                //1.6
                Console.WriteLine("Введите одну букву английского алфаита: ");
                char symbol;
                while (true)
                {
                    string input = Console.ReadLine();
                    if (input != null && input.Length == 1)
                    {
                        symbol = Convert.ToChar(input);
                        break;
                    }
                    Console.WriteLine("неверный ввод, попробуйте ещё раз: ");
                }
                if (taa.isUpperCase(symbol)) Console.WriteLine(symbol + " - заглаваня буква.");
                else Console.WriteLine(symbol + " - строчная буква английского алфавита или другой символ.");
                break;
            case 4:
                //1.8
                int a = 0, b = 0;
                a = enterNum();
                b = enterNum();
                if (taa.isDivisor(a, b)) Console.WriteLine("одно из двух чисел - делитель другого.");
                else Console.WriteLine("числа не деляться без остатка друг на друга");
                break;
            case 5:
                //1.10
                a = enterNum();
                b = enterNum();
                for (int i = 0; i < 4; i++)
                {
                    a = taa.lastNumSum(a, b);
                    if (i != 3) b = enterNum();
                }
                Console.WriteLine("ответ: " + a);
                break;
            case 6:
                //2.2
                int x = 0, y = 0;
                x = enterNum();
                y = enterNum();
                double answer = taa.safeDiv(x, y);
                if (answer == 0 && x != 0) Console.WriteLine("деление на 0");
                else Console.WriteLine(x + " разделить на " + y + " = " + answer);
                break;
            case 7:
                //2.4
                x = enterNum();
                y = enterNum();
                Console.WriteLine(taa.makeDecision(x, y));
                break;
            case 8:
                //2.6
                int z = 0;
                x = enterNum();
                y = enterNum();
                z = enterNum();
                if (taa.sum3(x, y, z)) Console.WriteLine("два числа в сумме дают третье.");
                else Console.WriteLine("нет пары числел сумма которых дает третье.");
                break;
            case 9:
                //2.8
                x = enterNum(0);
                string result = taa.age(x);
                Console.WriteLine(result);
                break;
            case 10:
                //2.10
                Console.WriteLine("Введите день недели: ");
                string day = Console.ReadLine();
                while (day == null)
                {
                    Console.WriteLine("Введите день недели: ");
                    day = Console.ReadLine();
                }
                taa.printDays(day);
                break;
            case 11:
                //3.2
                x = enterNum();
                string s = taa.reverseListNums(x);
                Console.WriteLine("Результат: " + s);
                break;
            case 12:
                //3.4
                x = enterNum();
                y = enterNum(0);
                answer = taa.pow(x, y);
                Console.WriteLine("{0} в степени {1} = {2}.", x, y, answer);
                break;
            case 13:
                //3.6
                x = enterNum();
                if (taa.equalNum(x)) Console.WriteLine("все цифры в числе {0} однинаковы.", x);
                else Console.WriteLine("в числе {0} есть разные цифры", x);
                break;
            case 14:
                //3.8
                x = enterNum(1);
                taa.leftTriangle(x);
                break;
            case 15:
                //3.10
                taa.guessGame();
                break;
            case 16:
                //4.2
                int[] arr = enterArray();
                Console.WriteLine("Введите элемент массива, позицию которого вы хотите найти.");
                x = enterNum();
                int index = taa.findLast(arr, x);
                if (index == -1) Console.WriteLine("Элемента {0} нет в массиве.", x);
                else Console.WriteLine("Последнее вхождение элемента {0} находиться в данном массиве по индексу - {1}", x, index);
                break;
            case 17:
                //4.4
                arr = enterArray();
                int pos = 0;
                Console.WriteLine("Ввод элемента для вставки в массив.");
                x = enterNum();
                Console.WriteLine("Ввод индекса вставки в массив.");
                pos = enterNum(0, arr.Length - 1);

                int[] newArr = taa.add(arr, x, pos);
                for (int i = 0; i < newArr.Length; i++) Console.Write(newArr[i] + " ");
                break;
            case 18:
                //4.6
                arr = enterArray();
                taa.reverse(arr);
                for (int i = 0; i < arr.Length; i++) Console.Write(arr[i] + " ");
                break;
            case 19:
                //4.8
                arr = enterArray();
                int[] arr2 = enterArray();
                newArr = taa.concat(arr, arr2);
                for (int i = 0; i < newArr.Length; i++) Console.Write(newArr[i] + " ");
                break;
            case 20:
                //4.10
                arr = enterArray();
                newArr = taa.deleteNegative(arr);
                for (int i = 0; i < newArr.Length; i++) Console.Write(newArr[i] + " ");
                break;
        }
    }

    private static int[] enterArray()
    {
        Console.WriteLine("Ввод размерности массива.");
        int x = enterNum(1);
        int[] arr = new int[x];
        for (int i = 0; i < arr.Length; i++)
        {
            Console.WriteLine("Ввод {0}-ого элемента массива: ", i);
            arr[i] = enterNum();
        }
        return arr;
    }

    private static int enterNum(int left, int right)
    {
        int n;
        Console.WriteLine("Введите число от {0} до {1}: ", left, right);
        while (true)
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out n) && n <= right && n >= left) return n;

            else
            {
                Console.WriteLine("неверный ввод");
                Console.WriteLine("введите число от {0} до {1} повторно: ", left, right);
            }

        }
    }
    private static int enterNum(int left)
    {
        int n;
        Console.WriteLine("Введите число от {0}: ", left);
        while (true)
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out n) && n >= left) return n;

            else
            {
                Console.WriteLine("неверный ввод");
                Console.WriteLine("введите число от {0} повторно: ", left);
            }

        }
    }

    private static int enterNumNotIncludingGap(int left, int right)
    {
        int n;
        Console.WriteLine("Введите число до {0} или от {1}: ", left, right);
        while (true)
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out n) && (n >= right || n <= left)) return n;

            else
            {
                Console.WriteLine("неверный ввод");
                Console.WriteLine("введите число до {0} или от {1} повторно: ", left, right);
            }

        }
    }
    public static int enterNum()
    {
        int n;
        Console.WriteLine("Введите число: ");
        while (true)
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out n)) return n;

            else
            {
                Console.WriteLine("неверный ввод");
                Console.WriteLine("введите число повторно: ");
            }

        }
    }

    //1.2
    public int sumLastNums(int x)
    {
        int digit1 = x % 100 / 10;
        int digit2 = x % 100 % 10;
        int s = digit1 + digit2;
        if (s >= 0) return s;
        else return -s;
    }

    //1.4
    public bool isPositive(int x)
    {
        return x > 0;
    }

    //1.6
    public bool isUpperCase(char x)
    {
        return (x >= 65 && x <= 90);
    }

    //1.8
    public bool isDivisor(int a, int b)
    {
        if (a == 0 && b == 0) return false;
        if (a == 0 || b == 0) return true;
        return (a % b == 0) || (b % a == 0);
    }

    //1.10
    public int lastNumSum(int a, int b)
    {
        return Math.Abs(a % 10) + Math.Abs(b % 10);
    }

    //2.2
    public double safeDiv(int x, int y)
    {
        if (y == 0)
            return 0;
        return (double)x / y;
    }

    //2.4
    public String makeDecision(int x, int y)
    {
        if (x > y) return string.Format("{0} > {1}", x, y);
        if (x < y) return string.Format("{0} < {1}", x, y);
        return string.Format("{0} == {1}", x, y);
    }

    //2.6
    public bool sum3(int x, int y, int z)
    {
        return (x + y == z || x + z == y || y + z == x);
    }

    //2.8
    public String age(int x)
    {
        string s = "";
        if (x % 10 == 1 && x != 11)
        {
            s = "год";
        }
        else if (x % 10 > 1 && x % 10 < 5 && (x > 15 || x < 11))
        {
            s = "года";
        }
        else s = "лет";

        return string.Format("{0} " + s, x);
    }

    //2.10  
    public void printDays(String x)
    {
        string[] days = { "понедельник", "вторник", "среда", "четверг", "пятница", "суббота", "воскресенье" };
        switch (x)
        {
            case "понедельник":
                for (int i = 1; i < 7; i++)
                {
                    Console.WriteLine(days[i]);
                }
                break;
            case "вторник":
                for (int i = 2; i < 7; i++)
                {
                    Console.WriteLine(days[i]);
                }
                break;
            case "среда":
                for (int i = 3; i < 7; i++)
                {
                    Console.WriteLine(days[i]);
                }
                break;
            case "четверг":
                for (int i = 4; i < 7; i++)
                {
                    Console.WriteLine(days[i]);
                }
                break;
            case "пятница":
                for (int i = 5; i < 7; i++)
                {
                    Console.WriteLine(days[i]);
                }
                break;
            case "суббота":
                for (int i = 6; i < 7; i++)
                {
                    Console.WriteLine(days[i]);
                }
                break;
            case "воскресенье":
                break;
            default:
                Console.Error.WriteLine("это не день недели");
                break;
        }
    }

    //3.2
    public String reverseListNums(int x)
    {
        String s = String.Format("{0}", x);
        if (x > 0)
        {
            for (int i = x - 1; i >= 0; i--)
            {
                s += " " + String.Format("{0}", i);
            }
        }
        if (x < 0)
        {
            for (int i = x + 1; i <= 0; i++)
            {
                s += " " + String.Format("{0}", i);
            }
        }
        return s;
    }

    //3.4
    public int pow(int x, int y)
    {
        int answer = 1;
        for (int i = 0; i < y; i++)
        {
            answer *= x;
        }

        return answer;
    }

    //3.6
    public bool equalNum(int x)
    {
        string s = String.Format("{0}", x);
        char digit = Convert.ToChar(String.Format("{0}", x % 10));
        return (s.Length == s.Count(c => c == digit));
    }

    //3.8
    public void leftTriangle(int x)
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j <= i; j++)
                Console.Write("*");
            Console.WriteLine();
        }
    }

    //3.10
    public void guessGame()
    {
        Random rnd = new Random();
        int guessNum = rnd.Next(0, 10);
        int userNum = -1;
        int i = 0;
        while (guessNum != userNum)
        {
            userNum = enterNum(0, 9);
            i++;
            if (guessNum == userNum)
            {
                Console.WriteLine("Вы угадали!");
            }
            else
            {
                Console.WriteLine("Вы не угадали.");
            }
        }
        Console.WriteLine("Вы угадали за {0} попытки", i);
    }

    //4.2
    public int findLast(int[] arr, int x)
    {
        int index = -1;
        for (int i = arr.Length - 1; i >= 0; i--)
        {
            if (arr[i] == x)
            {
                index = i;
                break;
            }
        }
        return index;
    }

    //4.4
    public int[] add(int[] arr, int x, int pos)
    {
        int[] newArr = new int[arr.Length + 1];
        int isAdd = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (i == pos)
            {
                newArr[i] = x;
                isAdd++;
            }
            newArr[i + isAdd] = arr[i];
        }
        return newArr;
    }

    //4.6
    public void reverse(int[] arr)
    {
        for (int i = arr.Length - 1; i > (arr.Length - 1) / 2; i--)
        {
            int buufer = arr[arr.Length - 1 - i];
            arr[arr.Length - 1 - i] = arr[i];
            arr[i] = buufer;
        }
    }

    //4.8
    public int[] concat(int[] arr1, int[] arr2)
    {
        int[] newArr = new int[arr1.Length + arr2.Length];
        for (int i = 0; i < arr1.Length; i++)
        {
            newArr[i] = arr1[i];
        }
        for (int i = 0; i < arr2.Length; i++)
        {
            newArr[i + arr1.Length] = arr2[i];
        }
        return newArr;
    }

    //4.10
    public int[] deleteNegative(int[] arr)
    {
        int length = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] >= 0)
                length++;
        }
        int[] newArr = new int[length];
        int j = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] >= 0)
            {
                newArr[j] = arr[i];
                j++;
            }
        }
        return newArr;
    }
}