using System;

class Multiply_Positive_Numbers
{
    static void Main()
    {
        Console.Write("Enter size of array: ");
        int size = Convert.ToInt32(Console.ReadLine());

        if (size < 0)
        {
            Console.WriteLine(-2);
            return;
        }

        int[] arr = new int[size];
        long product = 1;
        bool foundPositive = false;

        Console.WriteLine("Enter array elements:");
        for (int i = 0; i < size; i++)
        {
            arr[i] = Convert.ToInt32(Console.ReadLine());

            if (arr[i] > 0)
            {
                product *= arr[i];
                foundPositive = true;
            }
        }

        if (!foundPositive)
        {
            product = 0;
        }

        Console.WriteLine("Output:");
        Console.WriteLine(product);
    }
}
