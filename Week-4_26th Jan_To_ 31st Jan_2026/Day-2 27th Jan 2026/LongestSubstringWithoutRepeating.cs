using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.Write("Enter a string: ");
        string? s = Console.ReadLine();

        if (string.IsNullOrEmpty(s))
        {
            Console.WriteLine("Invalid input");
            return;
        }

        HashSet<char> set = new HashSet<char>();
        int left = 0, maxLength = 0, startIndex = 0;

        for (int right = 0; right < s.Length; right++)
        {
            while (set.Contains(s[right]))
            {
                set.Remove(s[left]);
                left++;
            }

            set.Add(s[right]);

            if (right - left + 1 > maxLength)
            {
                maxLength = right - left + 1;
                startIndex = left;
            }
        }

        Console.WriteLine("Length: " + maxLength);
        Console.WriteLine("Substring: " + s.Substring(startIndex, maxLength));
    }
}
