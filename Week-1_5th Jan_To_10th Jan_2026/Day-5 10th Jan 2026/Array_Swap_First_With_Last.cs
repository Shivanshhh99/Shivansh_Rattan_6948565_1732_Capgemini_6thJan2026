using System;

class Array_Swap_First_With_Last
{
    static void Main()
    {
        Console.Write("Enter the size of the array: ");
        int size = Convert.ToInt32(Console.ReadLine());

        if (size < 0)
        {
            Console.WriteLine("Output1: {-2}");
            return;
        }

        if (size % 2 == 0)
        {
            int[] outputEven = new int[size];
            outputEven[0] = -3;
            Console.WriteLine("Output1: {" + string.Join(",", outputEven) + "}");
            return;
        }

        int[] arr = new int[size];
        Console.WriteLine("Enter the elements of the array:");
        bool hasNegative = false;
        for (int i = 0; i < size; i++)
        {
            arr[i] = Convert.ToInt32(Console.ReadLine());
            if (arr[i] < 0)
                hasNegative = true;
        }

        if (hasNegative)
        {
            arr[0] = -1;
            Console.WriteLine("Output1: {" + string.Join(",", arr) + "}");
            return;
        }

        int mid = size / 2;
        int[] output = new int[size];

        output[0] = arr[size - 1];
        output[size - 1] = arr[0];

        for (int i = 1; i < size - 1; i++)
        {
            output[i] = arr[size - 1 - i];
        }

        Console.WriteLine("Output1: {" + string.Join(",", output) + "}");
    }
}
