using System;
using System.Collections.Generic;

class MahirlVowels
{
    static void Main()
    {
        Console.Write("Enter first word: ");
        string word1 = Console.ReadLine();

        Console.Write("Enter second word: ");
        string word2 = Console.ReadLine();

        HashSet<char> vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };

        HashSet<char> consonantsToRemove = new HashSet<char>();
        foreach (char c in word2)
        {
            char lower = char.ToLower(c);
            if (!vowels.Contains(lower))
                consonantsToRemove.Add(lower);
        }

        List<char> filtered = new List<char>();
        foreach (char c in word1)
        {
            char lower = char.ToLower(c);
            if (!consonantsToRemove.Contains(lower))
                filtered.Add(c);
        }

        if (filtered.Count == 0)
        {
            Console.WriteLine("");
            return;
        }

        List<char> result = new List<char> { filtered[0] };
        for (int i = 1; i < filtered.Count; i++)
        {
            if (char.ToLower(filtered[i]) != char.ToLower(filtered[i - 1]))
            {
                result.Add(filtered[i]);
            }
        }

        Console.WriteLine(new string(result.ToArray()));
    }
}
