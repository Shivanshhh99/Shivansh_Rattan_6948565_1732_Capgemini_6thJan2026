using System;

namespace StringCharacterInsertion
{
    class CharacterInserter
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the original string:");
            string originalString = Console.ReadLine();

            Console.WriteLine("Enter the character to insert:");
            char charToInsert = Convert.ToChar(Console.ReadLine());

            Console.WriteLine("Enter the position to insert the character (starting from 0):");
            int position = Convert.ToInt32(Console.ReadLine());

            // Validate position
            if (position < 0 || position > originalString.Length)
            {
                Console.WriteLine("Invalid position. It should be between 0 and " + originalString.Length);
                return;
            }

            // Insert character
            string updatedString = originalString.Insert(position, charToInsert.ToString());

            Console.WriteLine("Updated string: " + updatedString);
        }
    }
}
