using System;
using System.Text.RegularExpressions;

namespace WordValidatorApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Input string
            Console.WriteLine("Enter a string of words:");
            string input = Console.ReadLine();

            int validWordCount = CountValidWords(input);

            Console.WriteLine("Number of valid words: " + validWordCount);
        }

        static int CountValidWords(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return 0;

            string[] words = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int count = 0;

            foreach (string word in words)
            {
                if (IsValidWord(word))
                    count++;
            }

            return count;
        }

        static bool IsValidWord(string word)
        {
            // Rule 1: Length must be greater than 2
            if (word.Length <= 2)
                return false;

            // Rule 4: Only alphabets or alphanumeric
            if (!Regex.IsMatch(word, @"^[a-zA-Z0-9]+$"))
                return false;

            // Rule 2: At least one vowel
            string vowels = "aeiouAEIOU";
            bool hasVowel = false;
            bool hasConsonant = false;

            foreach (char c in word)
            {
                if (vowels.Contains(c))
                    hasVowel = true;
                else if (char.IsLetter(c))
                    hasConsonant = true;
            }

            return hasVowel && hasConsonant;
        }
    }
}
