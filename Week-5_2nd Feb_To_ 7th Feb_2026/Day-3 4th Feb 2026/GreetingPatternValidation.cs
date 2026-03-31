using System;
using System.Text.RegularExpressions;

namespace GreetingPatternValidation
{
    class GreetingMessageValidator
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Greeting Message :"); //Hi how are you dear
            string greetingMessage = Console.ReadLine();

            Console.WriteLine("Enter Name (More than 15 characters):");
            string userName = Console.ReadLine();

            // Combine input
            string finalMessage = greetingMessage + " " + userName;

            // Regex pattern for:
            // hi how are you dear + space + name
            string pattern = @"^hi how are you dear\s[A-Za-z]{16,}$";

            Match messageMatch = Regex.Match(finalMessage, pattern, RegexOptions.IgnoreCase);

            if (messageMatch.Success)
            {
                Console.WriteLine("Valid Pattern");
                Console.WriteLine("Output: " + finalMessage);
            }
            else
            {
                Console.WriteLine("Invalid Pattern. Ensure name is more than 15 characters and format is correct.");
            }
        }
    }
}
