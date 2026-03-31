using System;
using System.Text.RegularExpressions;

class DateExtraction
{
    static void Main()
    {
        Console.WriteLine("Enter the text:");
        string input = Console.ReadLine();

        // Regex pattern for dd/mm/yyyy format
        string pattern = @"\b\d{2}/\d{2}/\d{4}\b";

        // Find all matches
        MatchCollection matches = Regex.Matches(input, pattern);

        foreach (Match match in matches)
        {
            Console.WriteLine(match.Value);
        }
    }
}
