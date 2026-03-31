using System;
using System.Collections.Generic;

namespace MultiSubstringInserterApp
{
    class MultiSubstringInserter
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the original string:");
            string originalString = Console.ReadLine();

            Console.WriteLine("Enter the number of substrings to insert:");
            int n = Convert.ToInt32(Console.ReadLine());

            List<Tuple<int, string>> insertions = new List<Tuple<int, string>>();

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Enter substring {i + 1}:");
                string substring = Console.ReadLine();

                Console.WriteLine($"Enter position to insert substring {i + 1} (starting from 0):");
                int position = Convert.ToInt32(Console.ReadLine());

                insertions.Add(Tuple.Create(position, substring));
            }

            // Sort insertions by position (ascending) to maintain correct order
            insertions.Sort((a, b) => a.Item1.CompareTo(b.Item1));

            // Insert substrings
            string updatedString = originalString;
            int offset = 0; // Adjust positions as string length increases

            foreach (var insertion in insertions)
            {
                int pos = insertion.Item1 + offset;
                if (pos < 0) pos = 0;
                if (pos > updatedString.Length) pos = updatedString.Length;

                updatedString = updatedString.Substring(0, pos) + insertion.Item2 + updatedString.Substring(pos);

                offset += insertion.Item2.Length;
            }

            // Output result
            Console.WriteLine("Updated string: " + updatedString);
        }
    }
}
