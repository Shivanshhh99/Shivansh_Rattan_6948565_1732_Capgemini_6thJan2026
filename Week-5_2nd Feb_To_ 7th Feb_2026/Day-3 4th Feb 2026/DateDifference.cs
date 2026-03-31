using System;
using System.Globalization;

namespace DateDifferenceApplication
{
    class DateDifferenceCalculator
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter First Date (dd/MM/yyyy):");
            string firstDateInput = Console.ReadLine();

            Console.WriteLine("Enter Second Date (dd/MM/yyyy):");
            string secondDateInput = Console.ReadLine();

            // Converting string to DateTime
            DateTime firstDate = DateTime.ParseExact(firstDateInput, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime secondDate = DateTime.ParseExact(secondDateInput, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            // Calculating difference
            TimeSpan dateDifference = secondDate - firstDate;

            int totalDays = Math.Abs(dateDifference.Days);

            Console.WriteLine("Output: " + totalDays + " days");
        }
    }
}
