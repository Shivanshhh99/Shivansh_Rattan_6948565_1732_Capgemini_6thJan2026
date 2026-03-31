using System;
using System.Text.RegularExpressions;

class PasswordValidation
{
    static void Main()
    {
        // Ask user for password
        Console.WriteLine("Enter your password:");
        string password = Console.ReadLine();

        // Check password conditions
        bool isValid =
            password.Length >= 8 &&
            Regex.IsMatch(password, "[A-Z]") &&      // At least one uppercase letter
            Regex.IsMatch(password, "[a-z]") &&      // At least one lowercase letter
            Regex.IsMatch(password, "[0-9]") &&      // At least one digit
            Regex.IsMatch(password, "[@$!%*?&]");    // At least one special character

        if (isValid)
        {
            Console.WriteLine("Strong");
        }
        else
        {
            Console.WriteLine("Weak");
        }
    }
}
