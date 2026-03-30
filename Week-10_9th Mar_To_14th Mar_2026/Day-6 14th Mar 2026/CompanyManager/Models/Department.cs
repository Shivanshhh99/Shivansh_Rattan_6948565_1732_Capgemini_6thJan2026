using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}