using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        public string Name { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}