using System;

class LuckyStringChecker
{
    static void Main()
    {
        Console.Write("Enter length of substring (n): ");
        int n = int.Parse(Console.ReadLine());

        Console.Write("Enter the string: ");
        string s = Console.ReadLine();

        // Check invalid
        if (n > s.Length)
        {
            Console.WriteLine("Invalid");
            return;
        }

        bool isLucky = false;

        // Slide a window of length n over the string
        for (int i = 0; i <= s.Length - n; i++)
        {
            string substring = s.Substring(i, n);

            // Check if substring contains only P, S, G
            bool validChars = true;
            foreach (char c in substring)
            {
                if (c != 'P' && c != 'S' && c != 'G')
                {
                    validChars = false;
                    break;
                }
            }

            if (!validChars) continue;

            // Check for at least n/2 consecutive same letters
            int maxConsec = 1;
            int currentConsec = 1;

            for (int j = 1; j < substring.Length; j++)
            {
                if (substring[j] == substring[j - 1])
                {
                    currentConsec++;
                    if (currentConsec > maxConsec)
                        maxConsec = currentConsec;
                }
                else
                {
                    currentConsec = 1;
                }
            }

            if (maxConsec >= n / 2)
            {
                isLucky = true;
                break; // Found lucky substring
            }
        }

        Console.WriteLine(isLucky ? "Yes" : "No");
    }
}
