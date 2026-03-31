using System;

namespace ConsecutiveVowelDeletionApp
{
    class VowelPairDeleter
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a string:");
            string input = Console.ReadLine();

            int deletions = DeleteConsecutiveVowelPairs(ref input);

            Console.WriteLine("Number of deletions made: " + deletions);
            Console.WriteLine("Updated string after deletions: " + input);
        }

        // Method to delete consecutive vowel pairs
        static int DeleteConsecutiveVowelPairs(ref string s)
        {
            int count = 0;
            string vowels = "aeiouAEIOU";
            System.Text.StringBuilder sb = new System.Text.StringBuilder(s);

            int i = 0;
            while (i < sb.Length - 1)
            {
                if (vowels.Contains(sb[i]) && vowels.Contains(sb[i + 1]))
                {
                    sb.Remove(i, 2); // Remove the pair
                    count++;
                    // No increment: new characters shift to current index
                }
                else
                {
                    i++;
                }
            }

            s = sb.ToString();
            return count;
        }
    }
}
