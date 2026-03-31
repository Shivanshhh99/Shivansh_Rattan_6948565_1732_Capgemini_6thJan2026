using System;
using System.Linq;

namespace ListDifferenceCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Input first list
            Console.WriteLine("Enter the first list of integers (comma-separated):");
            int[] firstList = Console.ReadLine().Split(',').Select(int.Parse).ToArray();

            // Input second list
            Console.WriteLine("Enter the second list of integers (comma-separated):");
            int[] secondList = Console.ReadLine().Split(',').Select(int.Parse).ToArray();

            // Process each number in the first list
            foreach (int number in firstList)
            {
                // Find all occurrences of number in second list
                int sumOccurrences = secondList.Where(x => x == number).Sum();

                // Print result
                Console.WriteLine(number + "-" + sumOccurrences);
            }
        }
    }
}
