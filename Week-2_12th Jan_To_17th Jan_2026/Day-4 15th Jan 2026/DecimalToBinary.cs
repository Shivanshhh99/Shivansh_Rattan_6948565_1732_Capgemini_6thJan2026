using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.Write("Enter a decimal number: ");
        int input1 = Convert.ToInt32(Console.ReadLine());

        int[] result = DecimalToBinary(input1);

        Console.WriteLine("Output:");
        foreach (int bit in result)
        {
            Console.Write(bit + " ");
        }
    }

    static int[] DecimalToBinary(int input1)
    {
        if (input1 < 0)
        {
            return new int[] { -1 };
        }

        if (input1 == 0)
        {
            return new int[] { 0 };
        }

        List<int> binary = new List<int>();

        while (input1 > 0)
        {
            binary.Add(input1 % 2);
            input1 /= 2;
        }

        binary.Reverse();

        return binary.ToArray();
    }
}
