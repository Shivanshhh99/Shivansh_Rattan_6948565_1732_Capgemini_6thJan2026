using System;

class CountOfElements
{
    public static int GetCount(int size, string[] input1, char input2)
    {
        int count = 0;

        for (int i = 0; i < size; i++)
        {
            // Check for non-alphabet characters
            foreach (char c in input1[i])
            {
                if (!char.IsLetter(c))
                {
                    return -2;
                }
            }

            // Check starting character (case-insensitive)
            if (char.ToLower(input1[i][0]) == char.ToLower(input2))
            {
                count++;
            }
        }

        if (count == 0)
            return -1;

        return count;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter the size : ");
        int size = Convert.ToInt32(Console.ReadLine());
        string[] arr = new string[size];

        Console.WriteLine("Enter the elements : ");

        for (int i = 0; i < size; i++)
        {
            arr[i] = Console.ReadLine();
        }

        Console.WriteLine("Enter the character : ");
        char ch = Convert.ToChar(Console.ReadLine());

        int result = CountOfElements.GetCount(size, arr, ch);

        if (result == -1)
            Console.WriteLine("No elements Found");
        else if (result == -2)
            Console.WriteLine("Only alphabets should be given");
        else
            Console.WriteLine(result);
    }
}
