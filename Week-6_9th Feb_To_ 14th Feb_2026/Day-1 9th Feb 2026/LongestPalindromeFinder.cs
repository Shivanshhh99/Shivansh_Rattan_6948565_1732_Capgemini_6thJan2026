using System;

namespace LongestPalindromicSubstringApp
{
    class LongestPalindromeFinder
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Enter a string:");
            string input = Console.ReadLine();

            int maxLength = FindLongestPalindromicSubstringLength(input);

            
            Console.WriteLine("Length of longest palindromic substring: " + maxLength);
        }

        // Method to find length of longest palindromic substring
        static int FindLongestPalindromicSubstringLength(string s)
        {
            if (string.IsNullOrEmpty(s))
                return 0;

            int maxLength = 1;

            for (int i = 0; i < s.Length; i++)
            {
                // Odd length palindrome
                int len1 = ExpandAroundCenter(s, i, i);
                // Even length palindrome
                int len2 = ExpandAroundCenter(s, i, i + 1);

                int len = Math.Max(len1, len2);
                if (len > maxLength)
                    maxLength = len;
            }

            return maxLength;
        }

        // Helper method to expand around center and return length
        static int ExpandAroundCenter(string s, int left, int right)
        {
            while (left >= 0 && right < s.Length && s[left] == s[right])
            {
                left--;
                right++;
            }

            // Length of palindrome = right - left - 1
            return right - left - 1;
        }
    }
}
