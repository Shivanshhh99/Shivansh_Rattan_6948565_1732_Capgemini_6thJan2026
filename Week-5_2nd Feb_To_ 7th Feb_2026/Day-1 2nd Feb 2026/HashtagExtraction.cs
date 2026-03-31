using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter the text:");
        string input = Console.ReadLine();

        // Regex pattern to match hashtags
        // Hashtag starts with # followed by letters or digits
        string pattern = @"#[A-Za-z0-9_]+";

        // Find all hashtags
        MatchCollection matches = Regex.Matches(input, pattern);

        foreach (Match match in matches)
        {
            Console.WriteLine(match.Value);
        }
    }
}
