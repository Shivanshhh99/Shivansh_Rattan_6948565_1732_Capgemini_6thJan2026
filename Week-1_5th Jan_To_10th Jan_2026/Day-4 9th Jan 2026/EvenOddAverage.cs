using System;

class EvenOddAverage
{
    static void Main()
    {
        int output1;

        Console.Write("Enter size of array: ");
        int size = Convert.ToInt32(Console.ReadLine());

        if (size<0)
        {
            output1 = -2;
            Console.WriteLine("Output1 = " + output1);
            return;
        }

        int[] arr = new int[size];

        Console.WriteLine("Enter array elements:");
        for (int i = 0; i < size; i++)
        {
            arr[i] = Convert.ToInt32(Console.ReadLine());

            if (arr[i] < 0)
            {
                output1 = -1;
                Console.WriteLine("Output1 = " + output1);
                return;
            }
        }

        int sumEven = 0;
        int sumOdd = 0;

        foreach (int num in arr)
        {
            if (num % 2 == 0)
                sumEven += num;
            else
                sumOdd += num;
        }

        output1 = (sumEven + sumOdd) / 2;

        Console.WriteLine("Output1 = " + output1);
    }
}
