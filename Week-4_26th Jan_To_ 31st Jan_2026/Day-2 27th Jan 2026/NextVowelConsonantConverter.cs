using System;

class NextVowelConsonantConverter
{
    public static string nextString(string input1)
    {
        string vowelsLower = "aeiou";
        string vowelsUpper = "AEIOU";

        char[] result = new char[input1.Length];

        for (int i = 0; i < input1.Length; i++)
        {
            char ch = input1[i];

            if (!char.IsLetter(ch))
            {
                return "Invalid input";
            }

            if (vowelsLower.Contains(ch))
            {
                char next = ch;
                do
                {
                    next++;
                } while (vowelsLower.Contains(next));

                result[i] = next;
            }
            else if (vowelsUpper.Contains(ch))
            {
                char next = ch;
                do
                {
                    next++;
                } while (vowelsUpper.Contains(next));

                result[i] = next;
            }
            else if (char.IsLower(ch))
            {
                char next = ch;
                do
                {
                    next++;
                } while (!vowelsLower.Contains(next));

                result[i] = next;
            }
            else
            {
                char next = ch;
                do
                {
                    next++;
                } while (!vowelsUpper.Contains(next));

                result[i] = next;
            }
        }

        return new string(result);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter the string: ");
        string input = Console.ReadLine();

        string output = NextVowelConsonantConverter.nextString(input);
        Console.WriteLine(output);
    }
}
