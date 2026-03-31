using System;

class LuckyNumbers
{
    static int SumOfDigits(long num)
    {
        int sum = 0;
        while (num > 0)
        {
            sum += (int)(num % 10);
            num /= 10;
        }
        return sum;
    }

    static bool IsPrime(int num)
    {
        if (num <= 1) return false;
        if (num == 2) return true;
        if (num % 2 == 0) return false;

        for (int i = 3; i * i <= num; i += 2)
        {
            if (num % i == 0)
                return false;
        }
        return true;
    }

    static void Main()
    {
        int m = int.Parse(Console.ReadLine());
        int n = int.Parse(Console.ReadLine());

        int count = 0;

        for (int x = m; x <= n; x++)
        {
            if (!IsPrime(x))
            {
                int sX = SumOfDigits(x);
                long square = (long)x * x;
                int sSquare = SumOfDigits(square);

                if (sSquare == sX * sX)
                {
                    count++;
                }
            }
        }

        Console.WriteLine(count);
    }
}
