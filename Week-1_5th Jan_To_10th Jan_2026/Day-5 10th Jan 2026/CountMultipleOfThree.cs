using System;

class CountMultiplesOfThree
{
    static void Main()
    {
        Console.Write("Enter the size of the array: ");
        int size = Convert.ToInt32(Console.ReadLine());

        if (size < 0)
        {
            Console.WriteLine("Output: -1");
            return;
        }

        int[] arr = new int[size];
        bool hasNegative = false;
        int count = 0;

        Console.WriteLine("Enter the elements of the array:");
        for (int i = 0; i < size; i++)
        {
            arr[i] = Convert.ToInt32(Console.ReadLine());
            if (arr[i] < 0)
                hasNegative = true;
        }

        if (hasNegative)
        {
            Console.WriteLine("Output: -1");
            return;
        }

        for (int i = 0; i < size; i++)
        {
            if (arr[i] % 3 == 0)
                count++;
        }

        Console.WriteLine("Output: " + count);
    }
}
