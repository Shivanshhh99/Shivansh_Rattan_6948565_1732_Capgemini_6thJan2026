using System;
using System.Collections.Generic;
using System.Linq;

namespace MaximumSubsequenceScore
{
    internal class Program
    {
        static long MaxScore(int[] speed, int[] efficiency, int k)
        {
            int n = speed.Length;

            var combined = new List<(int eff, int spd)>();
            for (int i = 0; i < n; i++)
                combined.Add((efficiency[i], speed[i]));

            combined = combined.OrderByDescending(x => x.eff).ToList();

            PriorityQueue<int, int> minHeap = new PriorityQueue<int, int>();
            long speedSum = 0;
            long result = 0;

            foreach (var item in combined)
            {
                minHeap.Enqueue(item.spd, item.spd);
                speedSum += item.spd;

                if (minHeap.Count > k)
                    speedSum -= minHeap.Dequeue();

                if (minHeap.Count == k)
                    result = Math.Max(result, speedSum * item.eff);
            }

            return result;
        }

        static void Main()
        {
            int[] speed = { 2, 10, 3, 1, 5, 8 };
            int[] efficiency = { 5, 4, 3, 9, 7, 2 };
            int k = 2;

            Console.WriteLine(MaxScore(speed, efficiency, k));
        }
    }
}