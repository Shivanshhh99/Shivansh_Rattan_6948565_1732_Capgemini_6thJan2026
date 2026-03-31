using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CompanyEmployees.Models
{
    [Table("tblEmployees")]
    public class EmployeeModel
    {
        [Key]
        public int EmployeeId { get; set; }
        public string Name { get; set; }

        [Range(18, 60, ErrorMessage = "Age must be between 18 and 60")]
        public int Age { get; set; }

        public string Designation { get; set; }

        // Foreign Key
        //[Display(Name = "Company")]
        public int CompanyId { get; set; }

        // Navigation Property
        public Company Company { get; set; }
    }
}
//[Required(ErrorMessage = "Employee Name is required")]
//[StringLength(100)]

