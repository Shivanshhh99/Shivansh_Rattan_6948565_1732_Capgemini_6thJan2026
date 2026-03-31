using System;
using System.Collections.Generic;

class EmployeeDesignationFinder
{
    public static string[] GetEmployee(string[] input1, string input2)
    {
        // Check for special characters in input2
        foreach (char c in input2)
        {
            if (!char.IsLetter(c) && c != ' ')
                return new string[] { "Invalid Input" };
        }

        // Check for special characters in input1
        foreach (string s in input1)
        {
            foreach (char c in s)
            {
                if (!char.IsLetter(c) && c != ' ')
                    return new string[] { "Invalid Input" };
            }
        }

        List<string> employees = new List<string>();
        int sameDesignationCount = 0;
        int totalEmployees = input1.Length / 2;

        for (int i = 0; i < input1.Length; i += 2)
        {
            string employeeName = input1[i];
            string designation = input1[i + 1];

            if (designation.Equals(input2, StringComparison.OrdinalIgnoreCase))
            {
                employees.Add(employeeName);
                sameDesignationCount++;
            }
        }

        if (employees.Count == 0)
        {
            return new string[] { "No employee for " + input2 + " designation" };
        }

        if (sameDesignationCount == totalEmployees)
        {
            return new string[] { "All employees belong to same " + input2 + " designation" };
        }

        return employees.ToArray();
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter number of elements:");
        int n = Convert.ToInt32(Console.ReadLine());

        string[] inputArray = new string[n];

        Console.WriteLine("Enter employee names and designations:");
        for (int i = 0; i < n; i++)
        {
            inputArray[i] = Console.ReadLine();
        }

        Console.WriteLine("Enter designation to search:");
        string designation = Console.ReadLine();

        string[] result = EmployeeDesignationFinder.GetEmployee(inputArray, designation);

        foreach (string s in result)
        {
            Console.WriteLine(s);
        }
    }
}
