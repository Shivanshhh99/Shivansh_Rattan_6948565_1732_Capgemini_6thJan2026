using System;

class AscDsc
{
    static void Main()
    {
        Console.Write("Enter size of arrays: ");
        int size = Convert.ToInt32(Console.ReadLine());

        int[] output;

        if (size < 0)
        {
            output = new int[1];
            output[0] = -2;
            PrintArray(output);
            return;
        }

        int[] input1 = new int[size];
        int[] input2 = new int[size];
        output = new int[size];

        Console.WriteLine("Enter elements of first array:");
        for (int i = 0; i < size; i++)
        {
            input1[i] = Convert.ToInt32(Console.ReadLine());
            if (input1[i] < 0)
            {
                output[0] = -1;
                PrintArray(output);
                return;
            }
        }

        Console.WriteLine("Enter elements of second array:");
        for (int i = 0; i < size; i++)
        {
            input2[i] = Convert.ToInt32(Console.ReadLine());
            if (input2[i] < 0)
            {
                output[0] = -1;
                PrintArray(output);
                return;
            }
        }

        Array.Sort(input1);

        Array.Sort(input2);
        Array.Reverse(input2);

        for (int i = 0; i < size; i++)
        {
            output[i] = input1[i] * input2[size - 1 - i];
        }

        Console.WriteLine("Output:");
        PrintArray(output);
    }

    static void PrintArray(int[] arr)
    {
        foreach (int val in arr)
        {
            Console.Write(val + " ");
        }
    }
}
