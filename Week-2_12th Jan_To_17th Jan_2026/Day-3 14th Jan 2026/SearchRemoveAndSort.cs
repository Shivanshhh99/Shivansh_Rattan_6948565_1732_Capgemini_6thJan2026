using System;

class SearchRemoveAndSort
{
    static void Main()
    {
        Console.WriteLine("Enter the array size:");
        int input2 = Convert.ToInt32(Console.ReadLine());

        if (input2 < 0)
        {
            Console.WriteLine(-2);
            return;
        }

        int[] input1 = new int[input2];

        Console.WriteLine("Enter the elements:");
        for (int i = 0; i < input2; i++)
        {
            input1[i] = Convert.ToInt32(Console.ReadLine());
            if (input1[i] < 0)
            {
                Console.WriteLine(-1);
                return;
            }
        }

        Console.WriteLine("Enter the search element:");
        int input3 = Convert.ToInt32(Console.ReadLine());

        int index = -1;
        for (int i = 0; i < input2; i++)
        {
            if (input1[i] == input3)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
        {
            Console.WriteLine(-3);
            return;
        }

        int[] output1 = new int[input2 - 1];
        int k = 0;

        for (int i = 0; i < input2; i++)
        {
            if (i != index)
                output1[k++] = input1[i];
        }

        Array.Sort(output1);

        Console.WriteLine("Output array:");
        foreach (int x in output1)
        {
            Console.Write(x + " ");
        }
    }
}
