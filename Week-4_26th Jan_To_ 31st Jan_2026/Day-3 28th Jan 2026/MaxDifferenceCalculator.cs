using System;
using System.Collections.Generic;

class MaxDifferenceCalculator
{
    public static int diffIntArray(int[] input1)
    {
        int n = input1.Length;

        // Business Rule 2
        if (n == 1 || n > 10)
            return -2;

        HashSet<int> uniqueValues = new HashSet<int>();

        // Business Rule 1 & 3
        foreach (int num in input1)
        {
            if (num < 0)
                return -1;

            if (uniqueValues.Contains(num))
                return -3;

            uniqueValues.Add(num);
        }

        int minValue = input1[0];
        int maxDiff = input1[1] - input1[0];

        for (int i = 1; i < n; i++)
        {
            if (input1[i] - minValue > maxDiff)
                maxDiff = input1[i] - minValue;

            if (input1[i] < minValue)
                minValue = input1[i];
        }

        return maxDiff;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter number of elements:");
        int n = int.Parse(Console.ReadLine());

        int[] input1 = new int[n];

        Console.WriteLine("Enter array elements:");
        for (int i = 0; i < n; i++)
        {
            input1[i] = int.Parse(Console.ReadLine());
        }

        int result = MaxDifferenceCalculator.diffIntArray(input1);

        Console.WriteLine("Output:");
        Console.WriteLine(result);
    }
}
