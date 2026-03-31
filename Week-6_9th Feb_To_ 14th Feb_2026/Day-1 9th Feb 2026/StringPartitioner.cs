using System;

namespace AlphaNumericPartitioner
{
    class StringPartitioner
    {
        static void Main(string[] args)
        {
            // Input
            Console.WriteLine("Enter an alphanumeric string:");
            string input = Console.ReadLine();

            string left = "";   // For digits
            string right = "";  // For non-digits

            foreach (char ch in input)
            {
                if (char.IsDigit(ch))
                    left += ch;
                else
                    right += ch;
            }

            // Output partitions
            Console.WriteLine("Left (digits): " + left);
            Console.WriteLine("Right (letters and others): " + right);
        }
    }
}
