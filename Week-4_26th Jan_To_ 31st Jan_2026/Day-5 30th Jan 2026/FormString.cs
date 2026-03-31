using System;
using System.Text;

class UserProgramCode
{
    public static string formString(string[] input1, int input2)
    {
        StringBuilder result = new StringBuilder();

        foreach (string str in input1)
        {
            // Check for special characters
            foreach (char c in str)
            {
                if (!char.IsLetter(c))
                    return "-1";
            }

            // Pick nth character (1-based index)
            if (str.Length >= input2)
                result.Append(str[input2 - 1]);
            else
                result.Append('$');
        }

        return result.ToString();
    }
}

class FormString
{
    static void Main()
    {
        Console.WriteLine("Enter number of strings:");
        int k = Convert.ToInt32(Console.ReadLine());

        string[] inputArray = new string[k];

        for (int i = 0; i < k; i++)
        {
            Console.WriteLine($"Enter string {i + 1}:");
            inputArray[i] = Console.ReadLine();
        }

        Console.WriteLine("Enter position (n):");
        int n = Convert.ToInt32(Console.ReadLine());

        string output = UserProgramCode.formString(inputArray, n);

        Console.WriteLine("Output:");
        Console.WriteLine(output);
    }
}
