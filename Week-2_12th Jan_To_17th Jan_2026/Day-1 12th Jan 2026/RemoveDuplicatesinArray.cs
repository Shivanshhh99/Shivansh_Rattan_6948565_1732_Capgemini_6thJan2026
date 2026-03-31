using System;
using System.Collections.Generic;

class RemoveDuplicatesArray
{
    static void Main()
    {
        Console.Write("Enter the size of the array: ");
        int size = Convert.ToInt32(Console.ReadLine());

        if (size < 0)
        {
            Console.WriteLine("Output: {-1}");
            return;
        }

        int[] arr = new int[size];
        bool hasNegative = false;

        Console.WriteLine("Enter the elements of the array:");
        for (int i = 0; i < size; i++)
        {
            arr[i] = Convert.ToInt32(Console.ReadLine());
            if (arr[i] < 0)
                hasNegative = true;
        }

        List<int> outputList = new List<int>();

        if (hasNegative)
        {
            outputList.Add(-1);
        }
        else
        {
            HashSet<int> seen = new HashSet<int>();
            foreach (int num in arr)
            {
                if (!seen.Contains(num))
                {
                    seen.Add(num);
                    outputList.Add(num);
                }
            }
        }

        Console.WriteLine("Output: {" + string.Join(",", outputList) + "}");
    }
}
