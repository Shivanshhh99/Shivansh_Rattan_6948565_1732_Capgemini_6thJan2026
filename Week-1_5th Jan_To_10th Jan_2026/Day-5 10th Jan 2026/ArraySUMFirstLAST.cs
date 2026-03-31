using System;

class ArraySUMFirstLAST
{
    static void Main()
    {
        Console.Write("Enter the size of the arrays: ");
        int size = Convert.ToInt32(Console.ReadLine());

        if (size < 0)
        {
            int[] output = new int[1] { -2 };
            Console.WriteLine("Output: {" + string.Join(",", output) + "}");
            return;
        }

        int[] input1 = new int[size];
        int[] input2 = new int[size];

        Console.WriteLine("Enter elements of array 1:");
        bool hasNegative = false;
        for (int i = 0; i < size; i++)
        {
            input1[i] = Convert.ToInt32(Console.ReadLine());
            if (input1[i] < 0) hasNegative = true;
        }

        Console.WriteLine("Enter elements of array 2:");
        for (int i = 0; i < size; i++)
        {
            input2[i] = Convert.ToInt32(Console.ReadLine());
            if (input2[i] < 0) hasNegative = true;
        }

        int[] outputArr = new int[size];

        if (hasNegative)
        {
            outputArr[0] = -1;
            Console.WriteLine("Output: {" + string.Join(",", outputArr) + "}");
            return;
        }

        for (int i = 0; i < size; i++)
        {
            if (i == 0)
                outputArr[i] = input1[0] + input2[size - 1];
            else if (i == size - 1)
                outputArr[i] = input1[size - 1] + input2[0];
            else
                outputArr[i] = input1[i] + input2[i];
        }

        Console.WriteLine("Output: {" + string.Join(",", outputArr) + "}");
    }
}
