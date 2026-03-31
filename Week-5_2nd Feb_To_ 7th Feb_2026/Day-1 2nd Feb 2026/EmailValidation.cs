using System;
using System.Text.RegularExpressions;

class EmailValidation
{
    static void Main()
    {
        Console.WriteLine("Enter the email address:");
        string email = Console.ReadLine();

        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        if (Regex.IsMatch(email, pattern))
        {
            Console.WriteLine("Valid");
        }
        else
        {
            Console.WriteLine("Invalid");
        }
    }
}
