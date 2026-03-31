using System;
using System.Collections.Generic;

class RemoveNegativesAndSort
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
        Console.WriteLine("Enter the elements of the array:");
        for (int i = 0; i < size; i++)
        {
            arr[i] = Convert.ToInt32(Console.ReadLine());
        }

        List<int> positiveList = new List<int>();
        foreach (int num in arr)
        {
            if (num >= 0)
                positiveList.Add(num);
        }

        positiveList.Sort();

        Console.WriteLine("Output: {" + string.Join(",", positiveList) + "}");
    }
}
