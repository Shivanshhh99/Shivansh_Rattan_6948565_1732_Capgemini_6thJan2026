using System;

namespace WordPositionFinderApp
{
    class WordPositionFinder
    {
        static void Main(string[] args)
        {
            //Positoon of THE AND OF
            // Take number of lines
            Console.WriteLine("Enter number of lines (N):");
            int N = Convert.ToInt32(Console.ReadLine());

            for (int lineNumber = 1; lineNumber <= N; lineNumber++)
            {
                Console.WriteLine($"Enter line {lineNumber}:");
                string inputLine = Console.ReadLine();

                int posThe = inputLine.IndexOf("the", StringComparison.OrdinalIgnoreCase);
                int posOf = inputLine.IndexOf("of", StringComparison.OrdinalIgnoreCase);

                // If not found, IndexOf returns -1 automatically
                Console.WriteLine($"Position of 'the': {posThe}");
                Console.WriteLine($"Position of 'of': {posOf}");
            }
        }
    }
}
