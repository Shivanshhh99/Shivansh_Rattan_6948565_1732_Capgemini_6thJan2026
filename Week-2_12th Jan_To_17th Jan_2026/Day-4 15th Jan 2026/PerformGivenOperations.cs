using System;

class Program
{
    static int PerformOperation(int input1, int input2, int input3)
    {
        // B.R1: if both input1 and input2 are less than 0
        if (input1 < 0 && input2 < 0)
        {
            return -1;
        }

        if (input3 == 1)
            return input1 + input2;
        else if (input3 == 2)
            return input1 - input2;
        else if (input3 == 3)
            return input1 * input2;
        else if (input3 == 4)
            return input1 / input2;
        else
            return 0;
    }

    static void Main()
    {
        int input1 = Convert.ToInt32(Console.ReadLine());
        int input2 = Convert.ToInt32(Console.ReadLine());
        int input3 = Convert.ToInt32(Console.ReadLine());

        int output = PerformOperation(input1, input2, input3);

        Console.WriteLine(output);
    }
}
