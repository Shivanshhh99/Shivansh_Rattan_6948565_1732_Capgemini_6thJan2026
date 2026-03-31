using System;
using System.Collections.Generic;

class MahirlMath
{
    static void Main()
    {
        Console.Write("Enter target number N: ");
        int N = int.Parse(Console.ReadLine());

        int start = 10;

        if (N == start)
        {
            Console.WriteLine(0);
            return;
        }

        // BFS setup
        Queue<(int number, int steps)> queue = new Queue<(int, int)>();
        HashSet<int> visited = new HashSet<int>();

        queue.Enqueue((start, 0));
        visited.Add(start);

        while (queue.Count > 0)
        {
            var (current, steps) = queue.Dequeue();

            // Generate next numbers
            int[] nextNumbers = { current + 2, current - 1, current * 3 };

            foreach (int next in nextNumbers)
            {
                if (next == N)
                {
                    Console.WriteLine(steps + 1);
                    return;
                }

                // Avoid revisiting and negative numbers
                if (next > 0 && !visited.Contains(next))
                {
                    queue.Enqueue((next, steps + 1));
                    visited.Add(next);
                }
            }
        }

        // If unreachable (should not happen in this problem)
        Console.WriteLine(-1);
    }
}
