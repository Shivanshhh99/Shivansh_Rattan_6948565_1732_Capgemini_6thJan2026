using System;

class CalculateGrossSalary
{
    static double CalculateGrossSalary(double basicPay, int workingDays)
    {
        if (basicPay < 0)
            return -1;

        if (basicPay > 10000)
            return -2;

        if (workingDays > 31)
            return -3;

        double da = 0.75 * basicPay;
        double hra = 0.50 * basicPay;

        double grossSalary = basicPay + da + hra;

        return grossSalary;
    }

    static void Main()
    {
        Console.Write("Enter basic pay: ");
        double basicPay = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter number of working days: ");
        int workingDays = Convert.ToInt32(Console.ReadLine());

        double output = CalculateGrossSalary(basicPay, workingDays);

        Console.WriteLine(output);
    }
}
