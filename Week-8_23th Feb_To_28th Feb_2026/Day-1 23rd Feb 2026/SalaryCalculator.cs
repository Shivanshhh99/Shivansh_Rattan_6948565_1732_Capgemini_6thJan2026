namespace SalaryCalculator
{
    class InvalidDayException : Exception
    {
        public InvalidDayException(string msg) : base(msg) { }
    }

    class InvalidSalaryException : Exception
    {
        public InvalidSalaryException(string msg) : base(msg) { }
    }

    class SalaryData
    {
        public string name;
        public int daysInMonth;
        public double salary;
    }

    class Validator
    {
        public static string validateSalaryData(SalaryData s)
        {
            if (s.daysInMonth != 28 && s.daysInMonth != 30 && s.daysInMonth != 31)
                throw new InvalidDayException("Invalid Days");

            if (s.salary < 0 || s.salary > 1000000)
                throw new InvalidSalaryException("Invalid Salary");

            return "Valid Data";
        }

        public static double getTotalSalary(SalaryData s)
        {
            validateSalaryData(s);
            return s.salary;
        }
    }

    class Program
    {
        static void Main()
        {
            SalaryData s = new SalaryData
            {
                name = "John",
                daysInMonth = 30,
                salary = 50000
            };

            try
            {
                Console.WriteLine(Validator.validateSalaryData(s));
                Console.WriteLine("Total Salary: " + Validator.getTotalSalary(s));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}