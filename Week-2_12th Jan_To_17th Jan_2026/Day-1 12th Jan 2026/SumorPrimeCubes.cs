using System;

class SumOfCubesPrimes
{
    static bool IsPrime(int num)
    {
        if (num <= 1) return false;
        for (int i = 2; i * i <= num; i++)
        {
            if (num % i == 0) return false;
        }
        return true;
    }

    static void Main()
    {
        Console.Write("Enter the value of n: ");
        int n = Convert.ToInt32(Console.ReadLine());
        int output;

        if (n < 0)
        {
            output = -1;
        }
        else if (n > 32676)
        {
            output = -2;
        }
        else
        {
            output = 0;
            for (int i = 2; i <= n; i++)
            {
                if (IsPrime(i))
                {
                    output += i * i * i;
                }
            }
        }

        Console.WriteLine("Output: " + output);
    }
}
