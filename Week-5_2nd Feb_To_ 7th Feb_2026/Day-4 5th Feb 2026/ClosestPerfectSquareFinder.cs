using System;

namespace ClosestPerfectSquareApp
{
    class ClosestPerfectSquareFinder
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a positive integer (1 to 7 digits):");
            string inputNumber = Console.ReadLine();

            // Validate input
            if (!int.TryParse(inputNumber, out int number) || number < 0 || inputNumber.Length > 7)
            {
                Console.WriteLine("Invalid input. Please enter a positive integer with up to 7 digits.");
                return;
            }

            // Find closest perfect square
            int closestPerfectSquare = FindClosestPerfectSquare(number);

            Console.WriteLine("Closest integer with a whole number square root: " + closestPerfectSquare);
        }

        // Method to find the closest perfect square
        static int FindClosestPerfectSquare(int n)
        {
            int lowerRoot = (int)Math.Floor(Math.Sqrt(n));
            int higherRoot = lowerRoot + 1;

            int lowerSquare = lowerRoot * lowerRoot;
            int higherSquare = higherRoot * higherRoot;

            // Determine which square is closer
            if ((n - lowerSquare) <= (higherSquare - n))
            {
                return lowerSquare;
            }
            else
            {
                return higherSquare;
            }
        }
    }
}
