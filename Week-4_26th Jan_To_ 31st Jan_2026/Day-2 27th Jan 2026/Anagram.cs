using System;
using System.Linq;

class Anagram
{
    static void Main()
    {
        Console.Write("Enter first string: ");
        string? str1 = Console.ReadLine();

        Console.Write("Enter second string: ");
        string? str2 = Console.ReadLine();

        if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
        {
            Console.WriteLine("Invalid input");
            return;
        }

        str1 = str1.Replace(" ", "").ToLower();
        str2 = str2.Replace(" ", "").ToLower();

        if (str1.Length != str2.Length)
        {
            Console.WriteLine("Not Anagrams");
            return;
        }

        char[] arr1 = str1.ToCharArray();
        char[] arr2 = str2.ToCharArray();

        Array.Sort(arr1);
        Array.Sort(arr2);

        if (arr1.SequenceEqual(arr2))
            Console.WriteLine("Strings are Anagrams");
        else
            Console.WriteLine("Strings are NOT Anagrams");
    }
}
