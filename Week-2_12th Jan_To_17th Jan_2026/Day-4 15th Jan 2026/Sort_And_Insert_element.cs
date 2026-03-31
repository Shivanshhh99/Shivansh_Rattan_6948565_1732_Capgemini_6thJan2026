using System;
using System.Collections.Generic;

class Sort_And_Insert_element
{
    static int[] SortAndInsert(int[] arr, int element)
    {
        if (arr == null || arr.Length <= 0)
        {
            return new int[] { -2 };
        }

        foreach (int num in arr)
        {
            if (num < 0)
            {
                return new int[] { -1 };
            }
        }

        List<int> list = new List<int>(arr);
        list.Add(element);
        list.Sort();

        return list.ToArray();
    }

    static void Main()
    {
        Console.Write("Enter array size: ");
        int size = Convert.ToInt32(Console.ReadLine());

        if (size <= 0)
        {
            Console.WriteLine("-2");
            return;
        }

        int[] arr = new int[size];

        Console.WriteLine("Enter array elements:");
        for (int i = 0; i < size; i++)
        {
            arr[i] = Convert.ToInt32(Console.ReadLine());
        }

        Console.Write("Enter element to insert: ");
        int element = Convert.ToInt32(Console.ReadLine());

        int[] result = SortAndInsert(arr, element);

        Console.WriteLine("Output:");
        Console.WriteLine(string.Join(" ", result));
    }
}
