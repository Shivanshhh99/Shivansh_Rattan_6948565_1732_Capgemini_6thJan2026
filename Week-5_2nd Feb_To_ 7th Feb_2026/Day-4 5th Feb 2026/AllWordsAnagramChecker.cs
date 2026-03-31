using System;
using System.Linq;

namespace AnagramCheckerApp
{
    class AllWordsAnagramChecker
    {
        static void Main(string[] args)
        {
            //eg: listen, silent
            Console.WriteLine("Enter words separated by commas (like: dusty,study,dust,stydy):");
            string input = Console.ReadLine();

            string[] words = input.Split(',');

            bool areAllAnagrams = CheckAllWordsAreAnagrams(words);

            Console.WriteLine("Are all words anagrams of each other? " + areAllAnagrams);
        }

        // Method to check if all words are anagrams of each other
        static bool CheckAllWordsAreAnagrams(string[] words)
        {
            if (words.Length < 2)
                return true; // Only one word, trivially anagram

            // Sort characters of the first word
            string sortedFirstWord = String.Concat(words[0].OrderBy(c => c));

            // Compare sorted version of each subsequent word
            foreach (string word in words)
            {
                if (String.Concat(word.OrderBy(c => c)) != sortedFirstWord)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
