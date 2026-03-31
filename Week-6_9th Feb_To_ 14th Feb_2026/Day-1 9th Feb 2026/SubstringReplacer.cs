using System;

namespace SubstringReplaceApp
{
    class SubstringReplacer
    {
        static void Main(string[] args)
        {
            // Take original string
            Console.WriteLine("Enter the original string:");
            string originalString = Console.ReadLine();

            // Take substring to remove
            Console.WriteLine("Enter the substring to remove:");
            string substringToRemove = Console.ReadLine();

            // Take new substring to insert
            Console.WriteLine("Enter the substring to insert:");
            string substringToInsert = Console.ReadLine();

            string updatedString = ReplaceSubstringAtPosition(originalString, substringToRemove, substringToInsert);

            // Output result
            Console.WriteLine("Updated string: " + updatedString);
        }

        // Method to remove a substring and insert a new substring at the same position
        static string ReplaceSubstringAtPosition(string original, string removeSubstring, string insertSubstring)
        {
            int index = original.IndexOf(removeSubstring);

            if (index == -1)
            {
                Console.WriteLine("Substring to remove not found.");
                return original;
            }

            // Remove the old substring
            string before = original.Substring(0, index);
            string after = original.Substring(index + removeSubstring.Length);

            // Insert the new substring
            return before + insertSubstring + after;
        }
    }
}
