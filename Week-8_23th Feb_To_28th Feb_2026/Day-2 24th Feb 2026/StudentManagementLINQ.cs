using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagementLINQ
{
    class Student
    {
        public int StudentID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int DepartmentID { get; set; }
    }

    class Department
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
    }

    class Course
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public int DepartmentID { get; set; }
    }

    class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int StudentID { get; set; }
        public int CourseID { get; set; }
        public string Grade { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>
            {
                new Student{ StudentID=1, Name="Harsh", Age=20, DepartmentID=1 },
                new Student{ StudentID=2, Name="Rahul", Age=23, DepartmentID=1 },
                new Student{ StudentID=3, Name="Priti", Age=22, DepartmentID=2 },
                new Student{ StudentID=4, Name="Deven", Age=24, DepartmentID=2 },
                new Student{ StudentID=5, Name="Diksha", Age=19, DepartmentID=3 }
            };

            List<Department> departments = new List<Department>
            {
                new Department{ DepartmentID=1, DepartmentName="Computer Science" },
                new Department{ DepartmentID=2, DepartmentName="Electronics" },
                new Department{ DepartmentID=3, DepartmentName="Mechanical" }
            };

            List<Course> courses = new List<Course>
            {
                new Course{ CourseID=1, CourseName="Databases", DepartmentID=1 },
                new Course{ CourseID=2, CourseName="Algorithms", DepartmentID=1 },
                new Course{ CourseID=3, CourseName="Circuits", DepartmentID=2 },
                new Course{ CourseID=4, CourseName="Thermodynamics", DepartmentID=3 }
            };

            List<Enrollment> enrollments = new List<Enrollment>
            {
                new Enrollment{ EnrollmentID=1, StudentID=1, CourseID=1, Grade="A" },
                new Enrollment{ EnrollmentID=2, StudentID=2, CourseID=1, Grade="B" },
                new Enrollment{ EnrollmentID=3, StudentID=2, CourseID=2, Grade="A" },
                new Enrollment{ EnrollmentID=4, StudentID=3, CourseID=3, Grade="C" },
                new Enrollment{ EnrollmentID=5, StudentID=4, CourseID=3, Grade="A" }
            };

            GetStudentsOlderThan21(students);
            GetCoursesByDepartment(courses, 1);
            GetStudentEnrollments(students, courses, enrollments);
            GroupStudentsByDepartment(students, departments);
            GetTopYoungestStudents(students);
            SummaryReport(students, departments, courses, enrollments);
        }

        static void GetStudentsOlderThan21(List<Student> students)
        {
            var result = students.Where(s => s.Age > 21);
            foreach (var s in result)
                Console.WriteLine(s.Name);
        }

        static void GetCoursesByDepartment(List<Course> courses, int deptId)
        {
            var result = courses.Where(c => c.DepartmentID == deptId);
            foreach (var c in result)
                Console.WriteLine(c.CourseName);
        }

        static void GetStudentEnrollments(List<Student> students, List<Course> courses, List<Enrollment> enrollments)
        {
            var result = students.Join(enrollments,
                                       s => s.StudentID,
                                       e => e.StudentID,
                                       (s, e) => new { s.Name, e.CourseID, e.Grade })
                                 .Join(courses,
                                       se => se.CourseID,
                                       c => c.CourseID,
                                       (se, c) => new { se.Name, c.CourseName, se.Grade });

            foreach (var item in result)
                Console.WriteLine(item.Name + " - " + item.CourseName + " - " + item.Grade);
        }

        static void GroupStudentsByDepartment(List<Student> students, List<Department> departments)
        {
            var result = departments.GroupJoin(students,
                                               d => d.DepartmentID,
                                               s => s.DepartmentID,
                                               (d, s) => new { d.DepartmentName, Students = s });

            foreach (var group in result)
            {
                Console.WriteLine(group.DepartmentName);
                foreach (var s in group.Students)
                    Console.WriteLine("  " + s.Name);
            }
        }

        static void GetTopYoungestStudents(List<Student> students)
        {
            var result = students.OrderBy(s => s.Age).Take(2);
            foreach (var s in result)
                Console.WriteLine(s.Name + " - " + s.Age);
        }

        static void SummaryReport(List<Student> students, List<Department> departments, List<Course> courses, List<Enrollment> enrollments)
        {
            Func<string, int> gradeMap = g => g == "A" ? 4 : g == "B" ? 3 : g == "C" ? 2 : 0;

            var report = students.GroupJoin(enrollments,
                                            s => s.StudentID,
                                            e => e.StudentID,
                                            (s, es) => new { s, es })
                                 .Select(x => new
                                 {
                                     x.s.Name,
                                     Department = departments.First(d => d.DepartmentID == x.s.DepartmentID).DepartmentName,
                                     CourseCount = x.es.Count(),
                                     AvgGrade = x.es.Any() ? x.es.Average(e => gradeMap(e.Grade)) : 0
                                 });

            foreach (var r in report)
                Console.WriteLine(r.Name + " | " + r.Department + " | Courses: " + r.CourseCount + " | Avg Grade: " + r.AvgGrade);

            Console.ReadLine();
        }
    }
}