using System;

namespace DigitSumCalculatorApp
{
    class DigitSumCalculator
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a positive integer (1 to 10 digits):");
            string inputNumber = Console.ReadLine();

            // Validate input
            if (!long.TryParse(inputNumber, out long number) || number < 0 || inputNumber.Length > 10)
            {
                Console.WriteLine("Invalid input. Please enter a positive integer with up to 10 digits.");
                return;
            }

            // Calculate sum of digits
            int sumOfDigits = CalculateDigitSum(inputNumber);

            Console.WriteLine("Sum of digits: " + sumOfDigits);
        }

        // Method to calculate sum of digits
        static int CalculateDigitSum(string numberString)
        {
            int sum = 0;
            foreach (char digitChar in numberString)
            {
                sum += digitChar - '0'; // Convert char to int
            }
            return sum;
        }
    }
}
