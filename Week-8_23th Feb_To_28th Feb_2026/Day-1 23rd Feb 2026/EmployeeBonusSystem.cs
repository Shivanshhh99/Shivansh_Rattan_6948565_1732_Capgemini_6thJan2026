namespace EmployeeBonusSystem
{
    class Employee
    {
        public string name;
        public string projectName;
        public int workingHrs;
        public int bonus;

        public string SetBonus()
        {
            if (projectName == "AI" && workingHrs > 100)
                bonus = 5000;
            else if (workingHrs > 50)
                bonus = 2000;
            else
                bonus = 1000;

            return "Bonus Set";
        }

        public string CheckName()
        {
            return name + " Bonus: " + bonus;
        }
    }

    class Program
    {
        static void Main()
        {
            Employee e = new Employee
            {
                name = "John",
                projectName = "AI",
                workingHrs = 120
            };

            e.SetBonus();
            Console.WriteLine(e.CheckName());
        }
    }
}