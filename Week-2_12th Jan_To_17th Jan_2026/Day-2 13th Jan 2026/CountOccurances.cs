using System;

class CountOccurances
{
    static void Main()
    {
        Console.Write("Enter array size: ");
        int input2 = Convert.ToInt32(Console.ReadLine());

        if (input2 < 0)
        {
            Console.WriteLine(-2);
            return;
        }

        int[] arr = new int[input2];

        Console.WriteLine("Enter array elements:");
        for (int i = 0; i < input2; i++)
        {
            arr[i] = Convert.ToInt32(Console.ReadLine());

            if (arr[i] < 0)
            {
                Console.WriteLine(-1);
                return;
            }
        }

        Console.Write("Enter search value: ");
        int input3 = Convert.ToInt32(Console.ReadLine());

        if (input3 < 0)
        {
            Console.WriteLine(-3);
            return;
        }

        int count = 0;

        for (int i = 0; i < input2; i++)
        {
            if (arr[i] == input3)
                count++;
        }

        Console.WriteLine("Output: " + count);
    }
}

