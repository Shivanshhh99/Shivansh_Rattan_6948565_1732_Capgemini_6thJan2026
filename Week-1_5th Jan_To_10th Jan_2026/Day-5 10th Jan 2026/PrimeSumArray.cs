using System;

class PrimeSumArray
{
    static bool IsPrime(int num)
    {
        if (num <= 1)
            return false;

        for (int i = 2; i * i <= num; i++)
        {
            if (num % i == 0)
                return false;
        }
        return true;
    }

    static int SumOfPrimes(int[] arr, int size)
    {
        if (size < 0)
            return -2;

        int sum = 0;
        for (int i = 0; i < size; i++)
        {
            if (arr[i] < 0)
                return -1;

            if (IsPrime(arr[i]))
                sum += arr[i];
        }

        if (sum == 0)
            return -3;

        return sum;
    }

    static void Main()
    {
        Console.Write("Enter the size of the array: ");
        int size = Convert.ToInt32(Console.ReadLine());

        if (size < 0)
        {
            Console.WriteLine("Output1: -2");
            return;
        }

        int[] arr = new int[size];
        Console.WriteLine("Enter the elements of the array:");
        for (int i = 0; i < size; i++)
        {
            arr[i] = Convert.ToInt32(Console.ReadLine());
            if (arr[i] < 0)
            {
                Console.WriteLine("Output1: -1");
                return;
            }
        }

        int output1 = SumOfPrimes(arr, size);
        Console.WriteLine("Output1: " + output1);
    }
}
