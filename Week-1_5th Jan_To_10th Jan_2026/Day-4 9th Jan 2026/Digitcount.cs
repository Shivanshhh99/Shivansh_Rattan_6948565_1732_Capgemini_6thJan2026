using System;

class DigitCount
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
        else
        {
            if (number == 0)
            {
                output1 = 1;
            }
            else
            {
                int count = 0;
                while (number > 0)
                {
                    count++;
                    number /= 10;
                }
                output1 = count;
            }
        }

        Console.WriteLine("Output1 = " + output1);
    }
}