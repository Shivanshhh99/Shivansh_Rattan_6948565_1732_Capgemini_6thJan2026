using System;
using System.Linq;
using System.Collections.Generic;

namespace UniqueWordsFinderApp
{
    class UniqueWordsFinder
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter words separated by commas (e.g., listen,silent,hello,world,abc,cba):");
            string input = Console.ReadLine();

            string[] words = input.Split(',');

            List<string> uniqueWords = FindUniqueWords(words);

            Console.WriteLine("Unique words (not anagrams or identical): [" + string.Join(", ", uniqueWords) + "]");
        }

        // Method to find unique words
        static List<string> FindUniqueWords(string[] words)
        {
            Dictionary<string, int> wordCounts = new Dictionary<string, int>();
            Dictionary<string, string> sortedWordMap = new Dictionary<string, string>();

            foreach (string word in words)
            {
                string sortedWord = String.Concat(word.OrderBy(c => c));

                sortedWordMap[word] = sortedWord;

                if (wordCounts.ContainsKey(sortedWord))
                {
                    wordCounts[sortedWord]++;
                }
                else
                {
                    wordCounts[sortedWord] = 1;
                }
            }

            List<string> uniqueWords = new List<string>();

            foreach (string word in words)
            {
                if (wordCounts[sortedWordMap[word]] == 1)
                {
                    uniqueWords.Add(word);
                }
            }

            return uniqueWords;
        }
    }
}
