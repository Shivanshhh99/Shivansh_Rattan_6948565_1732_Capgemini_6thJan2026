using System.Collections.Generic;

namespace UniversityManagementSystem.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        public string Name { get; set; }

        public decimal Budget { get; set; }

        public ICollection<Instructor> Instructors { get; set; }
    }
}