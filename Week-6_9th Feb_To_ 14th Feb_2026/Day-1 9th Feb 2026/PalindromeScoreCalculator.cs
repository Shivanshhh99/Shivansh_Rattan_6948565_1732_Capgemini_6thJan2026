using System;

namespace PalindromeScoreCalculator
{
    class PalindromeScore
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a string:");
            string inputString = Console.ReadLine();

            int totalScore = CalculatePalindromeScore(inputString);

            Console.WriteLine("Final score: " + totalScore);
        }

        // Method to calculate score based on palindromes
        static int CalculatePalindromeScore(string s)
        {
            int score = 0;
            int length = s.Length;

            // Check for palindromes of length 4
            for (int i = 0; i <= length - 4; i++)
            {
                string sub4 = s.Substring(i, 4);
                if (IsPalindrome(sub4))
                {
                    score += 5;
                }
            }

            // Check for palindromes of length 5
            for (int i = 0; i <= length - 5; i++)
            {
                string sub5 = s.Substring(i, 5);
                if (IsPalindrome(sub5))
                {
                    score += 10;
                }
            }

            return score;
        }

        // Helper method to check if a string is a palindrome
        static bool IsPalindrome(string str)
        {
            int left = 0;
            int right = str.Length - 1;

            while (left < right)
            {
                if (str[left] != str[right])
                {
                    return false;
                }
                left++;
                right--;
            }

            return true;
        }
    }
}
