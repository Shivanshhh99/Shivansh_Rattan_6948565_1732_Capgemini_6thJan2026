using System;

class TotalMarksValidator
{
    static void Main()
    {
        Console.Write("Enter marks for Type 1 question (X): ");
        int X = int.Parse(Console.ReadLine());

        Console.Write("Enter marks for Type 2 question (Y): ");
        int Y = int.Parse(Console.ReadLine());

        Console.Write("Enter number of Type 1 questions (n1): ");
        int n1 = int.Parse(Console.ReadLine());

        Console.Write("Enter number of Type 2 questions (n2): ");
        int n2 = int.Parse(Console.ReadLine());

        Console.Write("Enter total marks scored by student (M): ");
        int M = int.Parse(Console.ReadLine());

        bool found = false;
        int type1Correct = 0;
        int type2Correct = 0;

        // ---- Check all possible Type 1 answers (maximize Type 1) ----
        for (int x = n1; x >= 0; x--)
        {
            int remaining = M - x * X;

            if (remaining < 0) continue;

            if (remaining % Y == 0)
            {
                int y = remaining / Y;

                if (y <= n2)
                {
                    type1Correct = x;
                    type2Correct = y;
                    found = true;
                    break; // stop at first max Type 1 solution
                }
            }
        }

        // ---- Output result ----
        if (found)
        {
            Console.WriteLine("\nValid");
            Console.WriteLine(type1Correct);
            Console.WriteLine(type2Correct);
        }
        else
        {
            Console.WriteLine("\nInvalid");
        }
    }
}
