using System;

class SumAndAvgMultiplesOf5
{
    static void Main()
    {
        Console.Write("Enter the size of the array: ");
        int size = Convert.ToInt32(Console.ReadLine());

        if (size < 0)
        {
            Console.WriteLine("Output: -2");
            return;
        }

        int[] arr = new int[size];
        bool hasNegative = false;
        int sum = 0;
        int count = 0;

        Console.WriteLine("Enter the elements of the array:");
        for (int i = 0; i < size; i++)
        {
            arr[i] = Convert.ToInt32(Console.ReadLine());
            if (arr[i] < 0) hasNegative = true;

            if (arr[i] % 5 == 0)
            {
                sum += arr[i];
                count++;
            }
        }

        if (hasNegative)
        {
            Console.WriteLine("Output: -1");
            return;
        }

        double avg = (count > 0) ? (double)sum / count : 0;

        Console.WriteLine("Sum of multiples of 5: " + sum);
        Console.WriteLine("Average of multiples of 5: " + avg);
    }
}
