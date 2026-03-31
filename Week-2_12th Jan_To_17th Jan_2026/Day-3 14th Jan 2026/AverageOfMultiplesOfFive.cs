using System;

class AverageOfMultiplesOfFive
{
    static void Main()
    {
        Console.Write("Enter the Number: ");
        int input1 = Convert.ToInt32(Console.ReadLine());

        if (input1 < 0)
        {
            Console.WriteLine(-1);
            return;
        }

        if (input1 > 500)
        {
            Console.WriteLine(-2);
            return;
        }

        int sum = 0;
        int count = 0;

        for (int i = 5; i <= input1; i += 5)
        {
            sum += i;
            count++;
        }

        int output1 = sum / count;
        Console.WriteLine(output1);
    }
}
