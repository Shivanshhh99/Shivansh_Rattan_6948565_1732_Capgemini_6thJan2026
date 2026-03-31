using System;
using System.Collections.Generic;

class ValidParanthesis
{
    static void Main()
    {
        Console.Write("Enter parentheses string: ");
        string? s = Console.ReadLine();

        if (string.IsNullOrEmpty(s))
        {
            Console.WriteLine("Invalid input");
            return;
        }

        Stack<char> stack = new Stack<char>();

        foreach (char c in s)
        {
            if (!(c == '(' || c == ')' ||
                  c == '{' || c == '}' ||
                  c == '[' || c == ']'))
            {
                Console.WriteLine("Invalid input: only parentheses allowed");
                return;
            }

            if (c == '(' || c == '{' || c == '[')
            {
                stack.Push(c);
            }
            else
            {
                if (stack.Count == 0)
                {
                    Console.WriteLine("Invalid Parentheses");
                    return;
                }

                char top = stack.Pop();

                if ((c == ')' && top != '(') ||
                    (c == '}' && top != '{') ||
                    (c == ']' && top != '['))
                {
                    Console.WriteLine("Invalid Parentheses");
                    return;
                }
            }
        }

        Console.WriteLine(stack.Count == 0
            ? "Valid Parentheses"
            : "Invalid Parentheses");
    }
}
