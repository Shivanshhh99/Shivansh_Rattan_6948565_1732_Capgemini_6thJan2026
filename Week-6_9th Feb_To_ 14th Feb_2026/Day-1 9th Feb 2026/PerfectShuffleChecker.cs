using System;

namespace PerfectShuffleCheckerApp
{
    class PerfectShuffleChecker
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter string x:");
            string x = Console.ReadLine();

            Console.WriteLine("Enter string y:");
            string y = Console.ReadLine();

            Console.WriteLine("Enter string z:");
            string z = Console.ReadLine();

            bool isPerfectShuffle = CheckPerfectShuffle(x, y, z);

            // Output result
            Console.WriteLine("Is z a perfect shuffle of x and y? " + isPerfectShuffle);
        }

        // Method to check perfect shuffle
        static bool CheckPerfectShuffle(string x, string y, string z)
        {
            int m = x.Length;
            int n = y.Length;

            // Lengths must match
            if (z.Length != m + n)
                return false;

            int i = 0, j = 0, k = 0;

            while (k < z.Length)
            {
                if (i < m && z[k] == x[i])
                {
                    i++;
                }
                else if (j < n && z[k] == y[j])
                {
                    j++;
                }
                else
                {
                    return false;
                }
                k++;
            }

            // All characters of x and y must be used
            return i == m && j == n;
        }
    }
}
