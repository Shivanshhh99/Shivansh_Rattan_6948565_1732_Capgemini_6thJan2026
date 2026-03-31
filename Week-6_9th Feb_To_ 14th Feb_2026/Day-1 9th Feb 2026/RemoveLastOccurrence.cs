using System;

namespace RemoveLastOccurrenceApp
{
    class LastWordRemover
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the original string:");
            string inputString = Console.ReadLine();

            Console.WriteLine("Enter the word to remove:");
            string wordToRemove = Console.ReadLine();

            string updatedString = RemoveLastOccurrence(inputString, wordToRemove);

            Console.WriteLine("String after removing last occurrence of '" + wordToRemove + "':");
            Console.WriteLine(updatedString);
        }

        // Method to remove last occurrence of a word
        static string RemoveLastOccurrence(string input, string word)
        {
            // Find the last index of the word
            int lastIndex = input.LastIndexOf(word);

            if (lastIndex == -1)
            {
                // Word not found
                return input;
            }

            // Remove the word
            string before = input.Substring(0, lastIndex);
            string after = input.Substring(lastIndex + word.Length);

            // Return combined string (trim extra space if needed)
            return (before + after).Replace("  ", " "); // Avoid double spaces
        }
    }
}
