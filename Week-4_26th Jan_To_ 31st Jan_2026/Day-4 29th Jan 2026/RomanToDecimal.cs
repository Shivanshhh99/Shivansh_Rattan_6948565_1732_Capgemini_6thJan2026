using System;
using System.Collections.Generic;

class RomanToDecimal
{
    public static int convertRomanToDecimal(string roman)
    {
        if (string.IsNullOrEmpty(roman))
            return -1;

        Dictionary<char, int> values = new Dictionary<char, int>()
        {
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
            {'D', 500},
            {'M', 1000}
        };

        int total = 0;

        for (int i = 0; i < roman.Length; i++)
        {
            // Invalid character check
            if (!values.ContainsKey(roman[i]))
                return -1;

            int current = values[roman[i]];

            if (i + 1 < roman.Length && values.ContainsKey(roman[i + 1]))
            {
                int next = values[roman[i + 1]];
                if (current < next)
                    total -= current;
                else
                    total += current;
            }
            else
            {
                total += current;
            }
        }

        return total;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter the Roman Number: ");
        string input = Console.ReadLine();
        int result = RomanToDecimal.convertRomanToDecimal(input);
        Console.WriteLine(result);
    }
}
