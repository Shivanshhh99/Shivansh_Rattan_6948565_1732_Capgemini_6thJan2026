using System;
using System.Collections.Generic;

class SumLargestNumberCalculator
{
    public static int largestNumber(int[] input1)
    {
        bool hasNegative = false;
        bool hasInvalid = false;

        HashSet<int> unique = new HashSet<int>();

        foreach (int num in input1)
        {
            if (num < 0)
                hasNegative = true;
            else if (num == 0 || num > 100)
                hasInvalid = true;
            else
                unique.Add(num);
        }

        if (hasNegative && hasInvalid)
            return -3;
        if (hasNegative)
            return -1;
        if (hasInvalid)
            return -2;

        int sum = 0;

        for (int start = 1; start <= 91; start += 10)
        {
            int max = 0;

            foreach (int val in unique)
            {
                if (val >= start && val <= start + 9 && val > max)
                    max = val;
            }

            sum += max;
        }

        return sum;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter the size of array: ");
        int n = int.Parse(Console.ReadLine());

        int[] input1 = new int[n];

        Console.WriteLine("Enter the array elements:");
        for (int i = 0; i < n; i++)
        {
            Console.Write("Element " + (i + 1) + ": ");
            input1[i] = int.Parse(Console.ReadLine());
        }

        int result = SumLargestNumberCalculator.largestNumber(input1);

        Console.WriteLine("Output: " + result);
    }
}
