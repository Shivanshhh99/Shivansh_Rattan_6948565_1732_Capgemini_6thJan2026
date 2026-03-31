using System;

class CountNotes
{
    static void Main()
    {
        Console.Write("Enter the rupee amount: ");
        int amount = Convert.ToInt32(Console.ReadLine());
        int output;

        if (amount < 0)
        {
            output = -1;
        }
        else
        {
            int[] denominations = { 500, 100, 50, 10, 1 };
            int remaining = amount;
            output = 0;

            foreach (int denom in denominations)
            {
                int count = remaining / denom;
                remaining %= denom;
                output += count;
            }
        }

        Console.WriteLine("Output1: " + output);
    }
}
