using System;
using System.Text;

namespace AlternatingCharacterDeletion
{
    class AlternatingCharacterRemover
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a string:");
            string inputString = Console.ReadLine();

            string resultString = DeleteAlternatingCharacters(inputString);

            Console.WriteLine("String after deleting alternating characters: " + resultString);
        }

        // Method to delete alternating characters
        static string DeleteAlternatingCharacters(string s)
        {
            StringBuilder newString = new StringBuilder();

            for (int i = 0; i < s.Length; i++)
            {
                // Keep characters at even indices (0, 2, 4…)
                if (i % 2 == 0)
                {
                    newString.Append(s[i]);
                }
            }

            return newString.ToString();
        }
    }
}
