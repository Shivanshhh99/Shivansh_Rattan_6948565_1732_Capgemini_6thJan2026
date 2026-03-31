using System;

namespace LongestNonDecreasingBinarySubstring
{
    class Program
    {
        static void Main(string[] args)
        {
            // Input binary string
            Console.WriteLine("Enter binary string:");
            string s = Console.ReadLine();

            int n = s.Length;

            // Count number of 0s and 1s
            int count0 = 0, count1 = 0;
            foreach (char c in s)
            {
                if (c == '0') count0++;
                else if (c == '1') count1++;
                else
                {
                    Console.WriteLine("Invalid character in input string. Only 0 and 1 allowed.");
                    return;
                }
            }

            // Longest non-decreasing substring after allowed swaps
            int longest = n; // All 0s first, then all 1s

            Console.WriteLine("Longest non-decreasing substring length: " + longest);
        }
    }
}
