using System;
using System.Text.RegularExpressions;

namespace InvoiceProcessingSystem
{
    class InvoiceNumberIncrementProgram
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Current Invoice Number (Format: CAP-XXX):");
            string currentInvoiceNumber = Console.ReadLine();

            Console.WriteLine("Enter Increment Value:");
            int incrementValue = Convert.ToInt32(Console.ReadLine());

            // Regular Expression to capture numeric part
            string pattern = @"^CAP-(\d+)$";
            Match invoiceMatch = Regex.Match(currentInvoiceNumber, pattern);

            if (invoiceMatch.Success)
            {
                // Extract numeric part
                string numericPart = invoiceMatch.Groups[1].Value;

                // Convert to integer
                int invoiceNumberValue = Convert.ToInt32(numericPart);

                // Add increment
                int updatedNumberValue = invoiceNumberValue + incrementValue;

                // Create updated invoice number
                string updatedInvoiceNumber = "CAP-" + updatedNumberValue;

                Console.WriteLine("Updated Invoice Number: " + updatedInvoiceNumber);
            }
            else
            {
                Console.WriteLine("Invalid Invoice Format. Please enter in CAP-XXX format.");
            }
        }
    }
}
