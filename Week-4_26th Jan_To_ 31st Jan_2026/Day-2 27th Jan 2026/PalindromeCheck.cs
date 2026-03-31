using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter a string: ");
        string? input = Console.ReadLine();

        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Invalid input");
            return;
        }

        string reversed = "";
        for (int i = input.Length - 1; i >= 0; i--)
        {
            reversed += input[i];
        }

        if (input.Equals(reversed, StringComparison.OrdinalIgnoreCase))
            Console.WriteLine("It is a Palindrome");
        else
            Console.WriteLine("It is NOT a Palindrome");
    }
}
