using System;

class CalculateSavings
{
    public int salary;
    public int days;

    public int savings()
    {
        Console.Write("Enter the Salary: ");
        salary = Convert.ToInt32(Console.ReadLine());

        if (salary > 9000)
            return -1;
        else if (salary < 0)
            return -2;

        Console.Write("How many days did you work: ");
        days = Convert.ToInt32(Console.ReadLine());

        if (days < 0)
            return -4;
        else if (days == 31)
            salary += 500;

        float expenditure = (0.50f * salary) + (0.20f * salary);
        return salary - (int)expenditure;
    }

    static void Main(string[] args)
    {
        CalculateSavings save = new CalculateSavings();
        int result = save.savings();
        Console.Write("Saving is: " + result);
    }
}
