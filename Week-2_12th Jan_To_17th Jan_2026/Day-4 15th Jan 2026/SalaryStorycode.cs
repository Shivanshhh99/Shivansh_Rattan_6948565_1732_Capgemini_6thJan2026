using System;

class SalaryStorycode
{
    static void Main()
    {
        Console.Write("Enter salary: ");
        int salary = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter number of working days: ");
        int days = Convert.ToInt32(Console.ReadLine());

        int result = CalculateSavings(salary, days);
        Console.WriteLine("Amount Saved: " + result);
    }

    static int CalculateSavings(int input1, int input2)
    {
        if (input1 > 9000)
            return -1;

        if (input1 < 0)
            return -3;

        if (input2 < 0)
            return -4;

        int totalSalary = input1;

        if (input2 == 31)
        {
            totalSalary += 500;
        }

        int foodExpense = (int)(0.5 * totalSalary);
        int travelExpense = (int)(0.2 * totalSalary);

        int savings = totalSalary - (foodExpense + travelExpense);

        return savings;
    }
}
