using System;

class ShipStorycode
{
    static void Main()
    {
        Console.Write("Enter array size: ");
        int size = Convert.ToInt32(Console.ReadLine());

        if (size <= 0)
        {
            Console.WriteLine("-2");
            return;
        }

        int[] arr1 = new int[size];
        int[] arr2 = new int[size];

        Console.WriteLine("Enter elements of first array:");
        for (int i = 0; i < size; i++)
        {
            arr1[i] = Convert.ToInt32(Console.ReadLine());
        }

        Console.WriteLine("Enter elements of second array:");
        for (int i = 0; i < size; i++)
        {
            arr2[i] = Convert.ToInt32(Console.ReadLine());
        }

        int[] result = ShipStory(arr1, arr2);

        Console.WriteLine("Output:");
        foreach (int val in result)
        {
            Console.Write(val + " ");
        }
    }

    static int[] ShipStory(int[] arr1, int[] arr2)
    {
        int n = arr1.Length;
        int[] output = new int[n];

        for (int i = 0; i < n; i++)
        {
            if (arr1[i] < 0 || arr2[i] < 0)
            {
                return new int[] { -1 };
            }
        }

        for (int i = 0; i < n; i++)
        {
            output[i] = arr1[i] + arr2[n - 1 - i];
        }

        return output;
    }
}
