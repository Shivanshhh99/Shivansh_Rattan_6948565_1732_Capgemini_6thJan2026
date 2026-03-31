using System;
using System.Linq;

class RemoveNegativeAndSort
{
    static void Main()
    {
        Console.WriteLine("Enter the number of elements:");
        int n = Convert.ToInt32(Console.ReadLine());

        if (n < 0)
        {
            Console.WriteLine("-1");
            return;
        }

        int[] arr = new int[n];

        Console.WriteLine("Enter the elements:");
        for (int i = 0; i < n; i++)
        {
            arr[i] = Convert.ToInt32(Console.ReadLine());
        }

        int[] result = arr.Where(x => x >= 0).ToArray();
        Array.Sort(result);

        Console.WriteLine("Result after removing negative numbers and sorting:");
        foreach (int x in result)
        {
            Console.Write(x + " ");
        }
    }
}
