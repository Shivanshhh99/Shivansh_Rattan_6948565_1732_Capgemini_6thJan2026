using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeSalaryManagement
{

    class Salary
    {
        public Dictionary<string, int> empList = new Dictionary<string, int>();

        public int TotalSalary()
        {
            return empList.Values.Sum();
        }

        public string GetSalary(string designation)
        {
            return empList.ContainsKey(designation)
                ? empList[designation].ToString()
                : "Designation not found";
        }

        public void UpdateSalary(string designation, int newSalary)
        {
            if (empList.ContainsKey(designation))
                empList[designation] = newSalary;
        }
    }

    class Program
    {
        static void Main()
        {
            Salary s = new Salary();
            s.empList.Add("Manager", 50000);
            s.empList.Add("Developer", 40000);

            Console.WriteLine("Total Salary: " + s.TotalSalary());
            Console.WriteLine("Manager Salary: " + s.GetSalary("Manager"));

            s.UpdateSalary("Manager", 60000);
            Console.WriteLine("Updated Total Salary: " + s.TotalSalary());
        }
    }
}