using System;

class AverageDivisibleByFive
{
    static void Main()
    {
        Console.WriteLine("Enter the limit:");
        int input1 = Convert.ToInt32(Console.ReadLine());

        if (input1 < 0)
        {
            Console.WriteLine("-1");
            return;
        }

        int sum = 0;
        int count = 0;

        for (int i = 5; i <= input1; i += 5)
        {
            sum += i;
            count++;
        }

        if (count == 0)
        {
            Console.WriteLine("0");
        }
        else
        {
            double average = (double)sum / count;
            Console.WriteLine("Average of numbers divisible by 5:");
            Console.WriteLine(average);
        }
    }
}
