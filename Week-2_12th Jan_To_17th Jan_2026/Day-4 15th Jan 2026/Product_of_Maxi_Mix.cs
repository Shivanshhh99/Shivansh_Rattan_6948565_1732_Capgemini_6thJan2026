using System;

class Product_of_Maxi_Mix
{
    static int FindProduct(int[] arr)
    {
        if (arr == null || arr.Length <= 0)
            return -2;

        int min = arr[0];
        int max = arr[0];

        foreach (int num in arr)
        {
            if (num < 0)
                return -1;

            if (num < min)
                min = num;

            if (num > max)
                max = num;
        }

        return min * max;
    }

    static void Main()
    {
        Console.Write("Enter the size of the array: ");
        int n = Convert.ToInt32(Console.ReadLine());

        if (n <= 0)
        {
            Console.WriteLine(-2);
            return;
        }

        int[] arr = new int[n];

        Console.WriteLine("Enter the elements of the array:");
        for (int i = 0; i < n; i++)
        {
            arr[i] = Convert.ToInt32(Console.ReadLine());
        }

        int result = FindProduct(arr);
        Console.WriteLine("Output is : " + result);
    }
}
