using System;

class Sumofmultiples
{
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

    static void Main()
    {
        int input1 = Convert.ToInt32(Console.ReadLine());
        int input2 = Convert.ToInt32(Console.ReadLine());

        int output = SumOfFactors(input1, input2);

        Console.WriteLine(output);
    }
}
