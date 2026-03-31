using System;

class Plot
{
    static void Main()
    {
        Console.Write("Enter array size: ");
        int size = Convert.ToInt32(Console.ReadLine());

        int result;

        if (size <= 0)
        {
            result = -2;
        }
        else
        {
            int[] arr = new int[size];

            Console.WriteLine("Enter array elements:");
            for (int i = 0; i < size; i++)
            {
                arr[i] = Convert.ToInt32(Console.ReadLine());
            }

            result = CalculatePassword(arr);
        }

        Console.WriteLine("Password: " + result);
    }

    static int CalculatePassword(int[] arr)
    {
        int sumEven = 0;
        int sumOdd = 0;

        foreach (int num in arr)
        {
            if (num < 0)
            {
                return -1;
            }

            if (num % 2 == 0)
                sumEven += num;
            else
                sumOdd += num;
        }

        int password = (sumEven + sumOdd) / 2;
        return password;
    }
}
