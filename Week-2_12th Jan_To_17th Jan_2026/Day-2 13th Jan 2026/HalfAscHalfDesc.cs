using System;

class HalfAscHalfDesc
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

        int mid = n / 2;

        Array.Sort(arr, 0, mid);

        Array.Sort(arr, mid, n - mid);
        Array.Reverse(arr, mid, n - mid);

        Console.WriteLine("Result:");
        foreach (int x in arr)
        {
            Console.Write(x + " ");
        }
    }
}
