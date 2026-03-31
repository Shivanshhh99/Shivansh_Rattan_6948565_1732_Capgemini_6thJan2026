using System;

class Product_of_digits_divisible_by_3or5
{
    static int FindOutput(int input1)
    {
        if (input1 < 0)
            return -1;

        if (input1 % 3 == 0 || input1 % 5 == 0)
            return -2;

        int product = 1;

        while (input1 > 0)
        {
            product *= input1 % 10;
            input1 /= 10;
        }

        if (product % 3 == 0 || product % 5 == 0)
            return 1;

        return 0;
    }

    static void Main()
    {
        Console.Write("Enter the number: ");
        int input1 = Convert.ToInt32(Console.ReadLine());

        int output = FindOutput(input1);
        Console.WriteLine("Output: " + output);
    }
}
