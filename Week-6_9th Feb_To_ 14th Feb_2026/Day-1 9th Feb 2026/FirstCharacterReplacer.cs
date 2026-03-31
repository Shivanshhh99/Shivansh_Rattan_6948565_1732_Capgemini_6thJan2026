using System;

namespace FirstCharacterReplacerApp
{
    class FirstCharacterReplacer
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the original string:");
            string inputString = Console.ReadLine();

            Console.WriteLine("Enter the character to replace:");
            char charToReplace = Convert.ToChar(Console.ReadLine());

            Console.WriteLine("Enter the character to replace with:");
            char replaceWith = Convert.ToChar(Console.ReadLine());

            string updatedString = ReplaceFirstOccurrence(inputString, charToReplace, replaceWith);

            Console.WriteLine($"String after replacing '{charToReplace}' with '{replaceWith}':");
            Console.WriteLine(updatedString);
        }

        // Method to replace first occurrence of a character
        static string ReplaceFirstOccurrence(string input, char oldChar, char newChar)
        {
            int index = input.IndexOf(oldChar);

            if (index == -1)
            {
                // Character not found
                return input;
            }

            // Replace character at index
            return input.Substring(0, index) + newChar + input.Substring(index + 1);
        }
    }
}
