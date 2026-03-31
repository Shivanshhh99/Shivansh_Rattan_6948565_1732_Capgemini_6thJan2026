using System;
using System.Text.RegularExpressions;

class PhoneNumberExtraction
{
    static void Main()
    {
        Console.WriteLine("Enter the text:");
        string input = Console.ReadLine();

        // Regex to match exactly 10-digit numbers
        string pattern = @"\b\d{10}\b";

        MatchCollection matches = Regex.Matches(input, pattern);

        foreach (Match match in matches)
        {
            Console.WriteLine(match.Value);
        }
    }
}
