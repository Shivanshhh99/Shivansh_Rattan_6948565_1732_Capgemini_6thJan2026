using System;

namespace Solution
{
    public abstract class Employee
    {
        protected string department;
        protected string name;
        protected string location;
        protected bool isOnVacation = false;

        public Employee(string department, string name, string location)
        {
            this.department = department;
            this.name = name;
            this.location = location;
        }

        public abstract string GetDepartment();
        public abstract string GetName();
        public abstract string GetLocation();
        public abstract bool GetStatus();
        public abstract void SwitchStatus();
    }

    class FinanceEmployee : Employee
    {
        public FinanceEmployee(string name, string location)
            : base("Finance", name, location)
        {
        }

        public override string GetDepartment()
        {
            return department;
        }

        public override string GetName()
        {
            return name;
        }

        public override string GetLocation()
        {
            return location;
        }

        public override bool GetStatus()
        {
            return isOnVacation;
        }

        public override void SwitchStatus()
        {
            isOnVacation = !isOnVacation;
        }
    }

    class MarketingEmployee : Employee
    {
        public MarketingEmployee(string name, string location)
            : base("Marketing", name, location)
        {
        }

        public override string GetDepartment()
        {
            return department;
        }

        public override string GetName()
        {
            return name;
        }

        public override string GetLocation()
        {
            return location;
        }

        public override bool GetStatus()
        {
            return isOnVacation;
        }

        public override void SwitchStatus()
        {
            isOnVacation = !isOnVacation;
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter Finance Employee Name and Location:");
            string str = Console.ReadLine();
            string[] strArr = str.Split(' ');

            Employee financeEmployee = new FinanceEmployee(strArr[0], strArr[1]);

            Console.WriteLine($"FinanceEmployee info: Department - {financeEmployee.GetDepartment()}, Name - {financeEmployee.GetName()}, Location - {financeEmployee.GetLocation()}");

            string status = financeEmployee.GetStatus() ? "on" : "not on";
            Console.WriteLine($"{financeEmployee.GetName()} is {status} vacation");

            Console.WriteLine("Switching");
            financeEmployee.SwitchStatus();

            status = financeEmployee.GetStatus() ? "on" : "not on";
            Console.WriteLine($"{financeEmployee.GetName()} is {status} vacation");

            Console.WriteLine("Switching");
            financeEmployee.SwitchStatus();

            status = financeEmployee.GetStatus() ? "on" : "not on";
            Console.WriteLine($"{financeEmployee.GetName()} is {status} vacation");


            Console.WriteLine("\nEnter Marketing Employee Name and Location:");
            str = Console.ReadLine();
            strArr = str.Split(' ');

            Employee marketingEmployee = new MarketingEmployee(strArr[0], strArr[1]);

            Console.WriteLine($"MarketingEmployee info: Department - {marketingEmployee.GetDepartment()}, Name - {marketingEmployee.GetName()}, Location - {marketingEmployee.GetLocation()}");

            status = marketingEmployee.GetStatus() ? "on" : "not on";
            Console.WriteLine($"{marketingEmployee.GetName()} is {status} vacation");

            Console.WriteLine("Switching");
            marketingEmployee.SwitchStatus();

            status = marketingEmployee.GetStatus() ? "on" : "not on";
            Console.WriteLine($"{marketingEmployee.GetName()} is {status} vacation");

            Console.WriteLine("Switching");
            marketingEmployee.SwitchStatus();

            status = marketingEmployee.GetStatus() ? "on" : "not on";
            Console.WriteLine($"{marketingEmployee.GetName()} is {status} vacation");
        }
    }
}