using System;

class CountVowels
{
    static void Main()
    {
        Console.Write("Enter a string: ");
        string input = Console.ReadLine();

        int vowelCount = 0;

        foreach (char c in input.ToLower())
        {
            if (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u')
            {
                vowelCount++;
            }
        }

        Console.WriteLine("Number of vowels: " + vowelCount);
    }
}
