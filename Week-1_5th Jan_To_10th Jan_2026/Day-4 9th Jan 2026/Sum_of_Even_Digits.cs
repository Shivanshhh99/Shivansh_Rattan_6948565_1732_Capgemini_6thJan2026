using System;

class Sum_of_Even_Digits
{
    static void Main()
    {
        Console.Write("Enter a number: ");
        int number = Convert.ToInt32(Console.ReadLine());

        int output;

        if (number < 0)
        {
            output = -1;
        }
        else if (number > 32767)
        {
            output = -2;
        }
        else
        {
            output = 0;

            while (number > 0)
            {
                int digit = number % 10;
                if (digit % 2 == 0)
                {
                    output += digit;
                }
                number /= 10;
            }
        }

        Console.WriteLine("Output: " + output);
    }
}
