using System;
using System.Linq;

namespace PipeSeparatedWordSorter
{
    class WordSorter
    {
        static void Main(string[] args)
        {
            // Input
            Console.WriteLine("Enter pipe-separated words (e.g., mango|apple|banana):");
            string input = Console.ReadLine();

            // Split the words
            string[] words = input.Split('|');

            // Sort words alphabetically
            Array.Sort(words, StringComparer.OrdinalIgnoreCase);

            // Join words back with pipe separator
            string sortedString = string.Join("|", words);

            // Output result
            Console.WriteLine("Sorted words: " + sortedString);
        }
    }
}
