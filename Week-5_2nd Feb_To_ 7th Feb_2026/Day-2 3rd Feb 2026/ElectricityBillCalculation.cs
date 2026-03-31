using System;
using System.Text;

namespace ElectricityBillingSystem
{
    class ElectricityBillCalculator
    {
        static void Main(string[] args)
        {
            // Taking inputs
            Console.WriteLine("Enter First Meter Reading (like:, AAAAA12345):");
            string firstMeterReading = Console.ReadLine();

            Console.WriteLine("Enter Second Meter Reading (like: AAAAA23456):");
            string secondMeterReading = Console.ReadLine();

            Console.WriteLine("Enter Rate Per Unit:");
            int ratePerUnit = Convert.ToInt32(Console.ReadLine());

            // Extract numeric parts
            string firstNumericPart = ExtractNumericPart(firstMeterReading);
            string secondNumericPart = ExtractNumericPart(secondMeterReading);

            int firstReadingValue = Convert.ToInt32(firstNumericPart);
            int secondReadingValue = Convert.ToInt32(secondNumericPart);

            // Calculate absolute difference
            int unitsConsumed = Math.Abs(secondReadingValue - firstReadingValue);

            // Calculate bill amount
            int billAmount = unitsConsumed * ratePerUnit;

            // Output result
            Console.WriteLine("Calculated Electricity Bill Amount: " + billAmount);
        }

        // Method to extract numeric part from alphanumeric string
        static string ExtractNumericPart(string meterReading)
        {
            StringBuilder numericPart = new StringBuilder();

            foreach (char character in meterReading)
            {
                if (char.IsDigit(character))
                {
                    numericPart.Append(character);
                }
            }

            return numericPart.ToString();
        }
    }
}
