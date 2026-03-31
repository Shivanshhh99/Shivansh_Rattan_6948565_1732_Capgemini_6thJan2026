using System;
using System.Collections.Generic;

// Base Class
class Person
{
    private int id;
    private string name;
    private string email;

    public Person(int id, string name, string email)
    {
        this.id = id;
        this.name = name;
        this.email = email;
    }

    public int GetId() => id;
    public string GetName() => name;
    public string GetEmail() => email;

    public virtual void DisplayProfile()
    {
        Console.WriteLine("\nID: " + id);
        Console.WriteLine("Name: " + name);
        Console.WriteLine("Email: " + email);
    }
}

// Student Class
class Student : Person
{
    private List<string> courses = new List<string>();

    public Student(int id, string name, string email)
        : base(id, name, email) { }

    public void EnrollCourse(string course)
    {
        courses.Add(course);
    }

    public override void DisplayProfile()
    {
        base.DisplayProfile();
        Console.WriteLine("Role: Student");
        Console.WriteLine("Enrolled Courses:");
        foreach (var c in courses)
            Console.WriteLine(" - " + c);
    }
}

// Professor Class
class Professor : Person
{
    private List<string> assignedCourses = new List<string>();

    public Professor(int id, string name, string email)
        : base(id, name, email) { }

    public void AssignCourse(string course)
    {
        assignedCourses.Add(course);
    }

    public override void DisplayProfile()
    {
        base.DisplayProfile();
        Console.WriteLine("Role: Professor");
        Console.WriteLine("Assigned Courses:");
        foreach (var c in assignedCourses)
            Console.WriteLine(" - " + c);
    }
}

// Staff Class
class Staff : Person
{
    private string department;

    public Staff(int id, string name, string email, string dept)
        : base(id, name, email)
    {
        department = dept;
    }

    public override void DisplayProfile()
    {
        base.DisplayProfile();
        Console.WriteLine("Role: Staff");
        Console.WriteLine("Department: " + department);
    }
}

// Course Class
class Course
{
    public string CourseCode { get; set; }
    public string CourseName { get; set; }
    public string Schedule { get; set; }

    public Course(string code, string name, string schedule)
    {
        CourseCode = code;
        CourseName = name;
        Schedule = schedule;
    }
}

// Department Class
class Department
{
    public string DepartmentName { get; set; }
    public List<Course> Courses { get; set; } = new List<Course>();

    public Department(string name)
    {
        DepartmentName = name;
    }

    public void AddCourse(Course course)
    {
        Courses.Add(course);
    }
}

// Main System
class UniversityEnrollmentSystem
{
    static void Main()
    {
        List<Student> students = new List<Student>();
        List<Professor> professors = new List<Professor>();
        List<Staff> staffMembers = new List<Staff>();

        int choice;

        do
        {
            Console.WriteLine("\n---- University Enrollment System ----");
            Console.WriteLine("1. Register Student");
            Console.WriteLine("2. Register Professor");
            Console.WriteLine("3. Register Staff");
            Console.WriteLine("4. Assign Course to Professor");
            Console.WriteLine("5. View Profile");
            Console.WriteLine("6. Exit");
            Console.Write("Enter choice: ");
            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter Student ID: ");
                    int sid = int.Parse(Console.ReadLine());
                    Console.Write("Enter Name: ");
                    string sname = Console.ReadLine();
                    Console.Write("Enter Email: ");
                    string semail = Console.ReadLine();

                    students.Add(new Student(sid, sname, semail));
                    Console.WriteLine("Student registered successfully.");
                    break;

                case 2:
                    Console.Write("Enter Professor ID: ");
                    int pid = int.Parse(Console.ReadLine());
                    Console.Write("Enter Name: ");
                    string pname = Console.ReadLine();
                    Console.Write("Enter Email: ");
                    string pemail = Console.ReadLine();

                    professors.Add(new Professor(pid, pname, pemail));
                    Console.WriteLine("Professor registered successfully.");
                    break;

                case 3:
                    Console.Write("Enter Staff ID: ");
                    int stid = int.Parse(Console.ReadLine());
                    Console.Write("Enter Name: ");
                    string stname = Console.ReadLine();
                    Console.Write("Enter Email: ");
                    string stemail = Console.ReadLine();
                    Console.Write("Enter Department: ");
                    string dept = Console.ReadLine();

                    staffMembers.Add(new Staff(stid, stname, stemail, dept));
                    Console.WriteLine("Staff registered successfully.");
                    break;

                case 4:
                    Console.Write("Enter Professor ID: ");
                    int profId = int.Parse(Console.ReadLine());
                    Console.Write("Enter Course Name: ");
                    string course = Console.ReadLine();

                    Professor prof = professors.Find(p => p.GetId() == profId);
                    if (prof != null)
                    {
                        prof.AssignCourse(course);
                        Console.WriteLine("Course assigned successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Professor not found.");
                    }
                    break;

                case 5:
                    Console.Write("Enter ID to view profile: ");
                    int viewId = int.Parse(Console.ReadLine());

                    Person person =
                        students.Find(s => s.GetId() == viewId) as Person ??
                        professors.Find(p => p.GetId() == viewId) as Person ??
                        staffMembers.Find(s => s.GetId() == viewId);

                    if (person != null)
                        person.DisplayProfile();
                    else
                        Console.WriteLine("Profile not found.");
                    break;

                case 6:
                    Console.WriteLine("Exiting system...");
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

        } while (choice != 6);
    }
}
