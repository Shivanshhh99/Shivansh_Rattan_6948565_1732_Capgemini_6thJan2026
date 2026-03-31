using System;

namespace SubstringInserterApp
{
    class SubstringInserter
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the original string:");
            string originalString = Console.ReadLine();

            Console.WriteLine("Enter the substring to insert:");
            string substringToInsert = Console.ReadLine();

            Console.WriteLine("Enter the position to insert at (starting from 0):");
            int position = Convert.ToInt32(Console.ReadLine());

            // Validate position
            if (position < 0 || position > originalString.Length)
            {
                Console.WriteLine("Invalid position. It should be between 0 and " + originalString.Length);
                return;
            }

            // Call function to insert substring
            string updatedString = InsertSubstring(originalString, substringToInsert, position);

            Console.WriteLine("Updated string: " + updatedString);
        }

        // Function to insert substring at specified position
        static string InsertSubstring(string original, string substring, int position)
        {
            return original.Substring(0, position) + substring + original.Substring(position);
        }
    }
}
