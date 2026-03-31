using System;

class DigitSumCalculator
{
    public static int SumOfDigits(string[] inputStrings)
    {
        int sum = 0;

        foreach (string str in inputStrings)
        {
            foreach (char ch in str)
            {
                if (char.IsDigit(ch))
                {
                    sum += ch - '0';
                }
                else if (!char.IsLetter(ch))
                {
                    // Special character found
                    return -1;
                }
            }
        }

        return sum;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter number of elements:");
        int n = Convert.ToInt32(Console.ReadLine());

        string[] inputArray = new string[n];

        Console.WriteLine("Enter the string elements:");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine("Enter element " + (i + 1) + ":");
            inputArray[i] = Console.ReadLine();
        }

        int result = DigitSumCalculator.SumOfDigits(inputArray);

        Console.WriteLine("Output:");
        Console.WriteLine(result);
    }
}
 