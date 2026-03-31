using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter size of array: ");
        int size = Convert.ToInt32(Console.ReadLine());

        int output;

        if (size < 0)
        {
            output = -2;
            Console.WriteLine("Output: " + output);
            return;
        }

        int[] arr = new int[size];

        Console.WriteLine("Enter array elements:");
        for (int i = 0; i < size; i++)
        {
            arr[i] = Convert.ToInt32(Console.ReadLine());

            if (arr[i] < 0)
            {
                output = -1;
                Console.WriteLine("Output: " + output);
                return;
            }
        }

        Console.Write("Enter element to search: ");
        int search = Convert.ToInt32(Console.ReadLine());

        output = 1;

        for (int i = 0; i < size; i++)
        {
            if (arr[i] == search)
            {
                output = i;
                break;
            }
        }

        Console.WriteLine("Output: " + output);
    }
}
