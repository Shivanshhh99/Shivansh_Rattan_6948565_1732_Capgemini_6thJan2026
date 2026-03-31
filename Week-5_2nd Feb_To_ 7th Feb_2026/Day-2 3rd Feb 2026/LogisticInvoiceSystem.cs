using System;
using System.Text.RegularExpressions;

namespace LogisticsInvoiceSystem
{
    class LocationCodeUpdaterProgram
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Current Invoice Number (Format: CAP-LOC-XXXX):");
            string currentInvoiceNumber = Console.ReadLine();

            Console.WriteLine("Enter New Location Code (e.g., HYD, BAN, DEL):");
            string newLocationCode = Console.ReadLine();

            // Regular expression pattern to capture location and numeric part
            string pattern = @"^(CAP-)([A-Z]+)(-\d+)$";
            Match invoiceMatch = Regex.Match(currentInvoiceNumber, pattern);

            if (invoiceMatch.Success)
            {
                string prefixPart = invoiceMatch.Groups[1].Value;   // CAP-
                string numericPart = invoiceMatch.Groups[3].Value;  // -XXXX

                // Create updated invoice number
                string updatedInvoiceNumber = prefixPart + newLocationCode + numericPart;

                Console.WriteLine("Updated Invoice Number: " + updatedInvoiceNumber);
            }
            else
            {
                Console.WriteLine("Invalid Invoice Format. Please use CAP-LOC-XXXX format.");
            }
        }
    }
}
