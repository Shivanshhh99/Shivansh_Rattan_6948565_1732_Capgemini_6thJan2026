using System;

class SumOfCubesOfPrime
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter the input value:");
        int input1 = Convert.ToInt32(Console.ReadLine());

        if (input1 < 0)
        {
            Console.WriteLine("-1");
            return;
        }

        if (input1 > 32767)
        {
            Console.WriteLine("-2");
            return;
        }

        long sum = 0;

        for (int i = 2; i <= input1; i++)
        {
            bool isPrime = true;

            for (int j = 2; j * j <= i; j++)
            {
                if (i % j == 0)
                {
                    isPrime = false;
                    break;
                }
            }

            if (isPrime)
            {
                sum += (long)i * i * i;
            }
        }

        Console.WriteLine("Sum of cubes of prime numbers:");
        Console.WriteLine(sum);
    }
}
