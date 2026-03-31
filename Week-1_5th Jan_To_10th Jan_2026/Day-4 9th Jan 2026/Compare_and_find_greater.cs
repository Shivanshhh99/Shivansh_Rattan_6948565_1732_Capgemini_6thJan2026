using System;

class Compare_and_find_greater
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

        int[] arr1 = new int[size];
        int[] arr2 = new int[size];
        output = new int[size];

        Console.WriteLine("Enter elements of first array:");
        for (int i = 0; i < size; i++)
        {
            arr1[i] = Convert.ToInt32(Console.ReadLine());
            if (arr1[i] < 0)
            {
                output[0] = -1;
                PrintArray(output);
                return;
            }
        }

        Console.WriteLine("Enter elements of second array:");
        for (int i = 0; i < size; i++)
        {
            arr2[i] = Convert.ToInt32(Console.ReadLine());
            if (arr2[i] < 0)
            {
                output[0] = -1;
                PrintArray(output);
                return;
            }
        }

        for (int i = 0; i < size; i++)
        {
            output[i] = arr1[i] > arr2[i] ? arr1[i] : arr2[i];
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
