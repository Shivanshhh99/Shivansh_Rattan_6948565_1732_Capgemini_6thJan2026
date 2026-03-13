using System;
using System.Collections.Generic;

namespace UniversityManagementSystem.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}