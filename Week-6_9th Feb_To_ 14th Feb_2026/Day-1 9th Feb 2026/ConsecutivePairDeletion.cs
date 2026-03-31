using System;

namespace ConsecutivePairDeletion
{
    class MaxConsecutivePairDeletion
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a string:");
            string inputString = Console.ReadLine();

            int maxDeletions = CalculateMaxDeletions(inputString);

            Console.WriteLine("Maximum number of deletions: " + maxDeletions);
        }

        // Method to calculate maximum deletions of consecutive character pairs
        static int CalculateMaxDeletions(string s)
        {
            int deletions = 0;
            int i = 0;

            while (i < s.Length - 1)
            {
                if (s[i] == s[i + 1])
                {
                    // Delete this pair by skipping two characters
                    deletions++;
                    i += 2;
                }
                else
                {
                    i++;
                }
            }

            return deletions;
        }
    }
}
