using System;

class ArmstrongNumber
{
    static void Main()
    {
        int number;
        int output1;

        Console.Write("Enter a number: ");
        number = Convert.ToInt32(Console.ReadLine());

        if (number < 0)
        {
            output1 = -1;
        }

        else if (number > 999)
        {
            output1 = -2;
        }
        else
        {
            int originalNumber = number;
            int sum = 0;

            while (number > 0)
            {
                int digit = number % 10;
                sum += digit * digit * digit;
                number /= 10;
            }

            if (sum == originalNumber)
                output1 = 1;
            else
                output1 = 0;
        }

        Console.WriteLine("Output1 = " + output1);
    }
}
