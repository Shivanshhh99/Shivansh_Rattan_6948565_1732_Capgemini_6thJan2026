using System;

class UserProgramCode
{
    public static string ReplaceString(string input1, int input2, char input3)
    {
        // Only alphabets and spaces allowed in input1
        foreach (char c in input1)
        {
            if (!char.IsLetter(c) && c != ' ')
                return "-1";
        }

        // input2 must be positive
        if (input2 <= 0)
            return "-2";

        // input3 must be a special character
        if (char.IsLetterOrDigit(input3))
            return "-3";

        string[] words = input1.Split(' ');

        // If nth word does not exist, return original string in lowercase
        if (input2 > words.Length)
            return input1.ToLower();

        // Replace characters of nth word
        words[input2 - 1] = new string(input3, words[input2 - 1].Length);

        return string.Join(" ", words).ToLower();
    }
}

class Program
{
    static void Main()
    {
        string input1 = Console.ReadLine();
        int input2 = Convert.ToInt32(Console.ReadLine());
        char input3 = Convert.ToChar(Console.ReadLine());

        string result = UserProgramCode.ReplaceString(input1, input2, input3);

        if (result == "-1")
            Console.WriteLine("Invalid String");
        else if (result == "-2")
            Console.WriteLine("Number not positive");
        else if (result == "-3")
            Console.WriteLine("Character not a special character");
        else
            Console.WriteLine(result);
    }
}
