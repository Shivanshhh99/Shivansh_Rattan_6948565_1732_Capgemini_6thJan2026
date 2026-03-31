using System;

namespace PipeSeparatedReverse
{
    class PipeWordReverser
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter pipe-separated words (like: apple|banana|cherry):");
            string input = Console.ReadLine();

            // Split words by pipe character
            string[] words = input.Split('|');

            // Reverse the array
            Array.Reverse(words);

            // Join back to a string using pipe
            string reversedString = string.Join("|", words);

            Console.WriteLine("Words in reverse order:");
            Console.WriteLine(reversedString);
        }
    }
}
