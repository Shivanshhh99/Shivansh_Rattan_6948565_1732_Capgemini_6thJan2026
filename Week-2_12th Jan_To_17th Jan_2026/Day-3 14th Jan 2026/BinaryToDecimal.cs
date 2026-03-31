using System;

class BinaryToDecimal
{
    static void Main()
    {
        Console.WriteLine("Enter the binary number:");
        string input1 = Console.ReadLine();

        foreach (char c in input1)
        {
            if (c != '0' && c != '1')
            {
                Console.WriteLine(-1);
                return;
            }
        }

        if (input1.Length > 5)
        {
            Console.WriteLine(-2);
            return;
        }
        
        int decimalValue = 0;
        int power = 1;

        for (int i = input1.Length - 1; i >= 0; i--)
        {
            if (input1[i] == '1')
                decimalValue += power;

            power *= 2;
        }

        Console.WriteLine(decimalValue);
    }
}
