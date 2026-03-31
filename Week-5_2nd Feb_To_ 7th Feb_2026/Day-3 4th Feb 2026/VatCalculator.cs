using System;

namespace VatCalculationSystem
{
    class ProductVatCalculator
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Product Type (M / V / C / D):");
            char productType = Convert.ToChar(Console.ReadLine().ToUpper());

            Console.WriteLine("Enter Product Price:");
            double productPrice = Convert.ToDouble(Console.ReadLine());

            double vatPercentage = 0;

            // Determining VAT percentage
            switch (productType)
            {
                case 'M':
                    vatPercentage = 5;
                    break;

                case 'V':
                    vatPercentage = 12;
                    break;

                case 'C':
                    vatPercentage = 6.25;
                    break;

                case 'D':
                    vatPercentage = 6;
                    break;

                default:
                    Console.WriteLine("Invalid Product Type.");
                    return;
            }

            // Calculating VAT amount
            double vatAmount = (productPrice * vatPercentage) / 100;

            Console.WriteLine("VAT Percentage: " + vatPercentage + "%");
            Console.WriteLine("VAT Amount: " + vatAmount);
        }
    }
}
