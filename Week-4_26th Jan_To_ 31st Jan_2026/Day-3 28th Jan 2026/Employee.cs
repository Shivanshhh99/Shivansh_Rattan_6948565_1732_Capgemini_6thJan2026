using System;
using System.Collections.Generic;
using System.Linq;

class Employee
{
    private string _name;
    private int _age;
    private string _designation;
    private string _city;

    // Default constructor
    public Employee()
    {
    }

    public Employee(string name, int age, string designation, string city)
    {
        _name = name;
        _age = age;
        _designation = designation;
        _city = city;
    }

    // Properties
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public int Age
    {
        get { return _age; }
        set { _age = value; }
    }

    public string Designation
    {
        get { return _designation; }
        set { _designation = value; }
    }

    public string City
    {
        get { return _city; }
        set { _city = value; }
    }

    // Override ToString
    public override string ToString()
    {
        return string.Format("{0,-21}{1,-6}{2,-21}{3,-20}",
                             _name, _age, _designation, _city);
    }
}

// EmployeeBO class
class EmployeeBO
{
    public void DisplayEmployeeDetails(List<Employee> employeeList, string name)
    {
        List<Employee> result = employeeList
                                .Where(e => e.Name == name)
                                .ToList();

        if (result.Count == 0)
        {
            Console.WriteLine("Employee named {0} not found", name);
        }
        else
        {
            Console.WriteLine("Name                 Age   Designation          City");
            foreach (Employee e in result)
            {
                Console.WriteLine(e);
            }
        }
    }

    public void DisplayYoungestEmployeeDetails(List<Employee> employeeList)
    {
        int minAge = employeeList.Min(e => e.Age);

        Employee youngest = employeeList
                            .Where(e => e.Age == minAge)
                            .First();

        Console.WriteLine("Name                 Age   Designation          City");
        Console.WriteLine(youngest);
    }

    public void displayEmployeesFromCity(List<Employee> employeeList, string cname)
    {
        List<Employee> result = employeeList
                                .Where(e => e.City == cname)
                                .ToList();

        if (result.Count == 0)
        {
            Console.WriteLine("City named {0} not found", cname);
        }
        else
        {
            Console.WriteLine("Name                 Age   Designation          City");
            foreach (Employee e in result)
            {
                Console.WriteLine(e);
            }
        }
    }
}

// Program class
class Program
{
    static void Main(string[] args)
    {
        List<Employee> employeeList = new List<Employee>();

        Console.WriteLine("Enter the number of employees");
        int noOfEmployees = int.Parse(Console.ReadLine());

        for (int i = 0; i < noOfEmployees; i++)
        {
            Console.WriteLine("Enter employee " + (i + 1) + " details:");
            Console.WriteLine("Enter the name");
            string name = Console.ReadLine();

            Console.WriteLine("Enter the age");
            int age = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the designation");
            string designation = Console.ReadLine();

            Console.WriteLine("Enter the city");
            string city = Console.ReadLine();

            employeeList.Add(new Employee(name, age, designation, city));
        }

        EmployeeBO employeeBO = new EmployeeBO();
        string option;

        do
        {
            Console.WriteLine("Enter your choice:");
            Console.WriteLine("1)Display Employee Details");
            Console.WriteLine("2)Display Youngest Employee Details");
            Console.WriteLine("3)Display Employees from City");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter employee name:");
                    string empName = Console.ReadLine();
                    employeeBO.DisplayEmployeeDetails(employeeList, empName);
                    break;

                case 2:
                    Console.WriteLine("The Youngest Employee Details");
                    employeeBO.DisplayYoungestEmployeeDetails(employeeList);
                    break;

                case 3:
                    Console.WriteLine("Enter city");
                    string cityName = Console.ReadLine();
                    employeeBO.displayEmployeesFromCity(employeeList, cityName);
                    break;
            }

            Console.WriteLine("Do you want to continue(Yes/No)?");
            option = Console.ReadLine();

        } while (option.Equals("Yes"));
    }
}
