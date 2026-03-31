using System;

namespace BallPassingGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // Input number of friends
            Console.WriteLine("Enter number of friends (N):");
            int N = Convert.ToInt32(Console.ReadLine());

            // Input total time (seconds)
            Console.WriteLine("Enter total seconds (T):");
            int T = Convert.ToInt32(Console.ReadLine());

            // Initial holder is friend 1
            int currentHolder = 1;
            int nextHolder = 1;

            for (int second = 1; second <= T; second++)
            {
                // Pass to next friend clockwise
                nextHolder = (currentHolder % N) + 1;

                // For last second, print who passed to whom
                if (second == T)
                {
                    Console.WriteLine($"At last second {T}: Friend {currentHolder} passed ball to Friend {nextHolder}");
                }

                // Update current holder
                currentHolder = nextHolder;
            }
        }
    }
}
