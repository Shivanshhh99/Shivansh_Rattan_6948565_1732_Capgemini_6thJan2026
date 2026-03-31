using System;

class PalindromeCheck
{
    static void Main()
    {
        Console.Write("Enter a number: ");
        int input = int.Parse(Console.ReadLine());

        int output = CheckPalindrome(input);

        Console.WriteLine("Output: " + output);
    }

    static int CheckPalindrome(int number)
    {
        if (number < 0)
        {
            return -1;
        }

        int originalNumber = number;
        int reversedNumber = 0;

        while (number > 0)
        {
            int digit = number % 10;
            reversedNumber = (reversedNumber * 10) + digit;
            number /= 10;
        }

        if (originalNumber == reversedNumber)
        {
            return 1;  
        }
        else
        {
            return -2;  
        }
    }
}
