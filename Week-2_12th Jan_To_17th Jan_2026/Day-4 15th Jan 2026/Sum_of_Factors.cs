using System;

class Sum_of_Factors
{
    static void Main()
    {
        Console.Write("Enter input1 (factor): ");
        int input1 = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter input2 (N): ");
        int input2 = Convert.ToInt32(Console.ReadLine());

        int result = SumOfFactors(input1, input2);
        Console.WriteLine("Output: " + result);
    }

    static int SumOfFactors(int input1, int input2)
    {
        if (input1 < 0)
            return -1;

        if (input2 > 32627)
            return -2;

        int sum = 0;

        for (int i = input1; i <= input2; i += input1)
        {
            sum += i;
        }

        return sum;
    }
}
