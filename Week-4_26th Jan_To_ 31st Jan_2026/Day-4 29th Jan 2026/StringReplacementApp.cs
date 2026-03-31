using System;
using System.Text.RegularExpressions;

class StringModifier
{
    public static string NegativeString(string input)
    {
        // Replace "is" only when it appears as a whole word
        return Regex.Replace(input, @"\bis\b", "is not");
    }
}

class StringReplacementApp
{
    static void Main()
    {
        Console.WriteLine("Enter the string : ");
        string input = Console.ReadLine();

        string result = StringModifier.NegativeString(input);

        Console.WriteLine(result);
    }
}
