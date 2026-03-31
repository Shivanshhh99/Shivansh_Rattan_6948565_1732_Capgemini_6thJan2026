using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.Write("Enter number of elements: ");
        int n = int.Parse(Console.ReadLine());

        int[] arr = new int[n];

        Console.WriteLine("Enter the elements:");
        for (int i = 0; i < n; i++)
        {
            arr[i] = int.Parse(Console.ReadLine());
        }

        Dictionary<int, int> freq = new Dictionary<int, int>();

        foreach (int num in arr)
        {
            if (freq.ContainsKey(num))
                freq[num]++;
            else
                freq[num] = 1;
        }

        int maxFreq = freq.Values.Max();

        List<int> output = new List<int>();
        foreach (var item in freq)
        {
            if (item.Value == maxFreq)
                output.Add(item.Key);
        }

        Console.WriteLine("Output: {" + string.Join(",", output) + "}");
    }
}
