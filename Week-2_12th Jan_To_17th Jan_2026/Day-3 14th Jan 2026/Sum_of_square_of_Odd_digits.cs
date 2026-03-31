using System;

class SumOfSquaresOfOddDigits
{
    static void Main()
    {
        Console.WriteLine("Enter the number:");
        int input1 = Convert.ToInt32(Console.ReadLine());

        if (input1 < 0)
        {
            Console.WriteLine("-1");
            return;
        }

        int sum = 0;

        while (input1 > 0)
        {
            int digit = input1 % 10;

            if (digit % 2 != 0)
            {
                sum += digit * digit;
            }

            input1 /= 10;
        }

        Console.WriteLine("Sum of squares of odd digits:");
        Console.WriteLine(sum);
    }
}
