using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementPortal.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Employee name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be 2–100 characters.")]
        [Display(Name = "Full Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Age is required.")]
        [Range(21, 65, ErrorMessage = "Age must be between 21 and 65.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Department is required.")]
        [Display(Name = "Department")]
        public string Department { get; set; } = string.Empty;

        [Display(Name = "Job Title")]
        public string JobTitle { get; set; } = string.Empty;

        [Display(Name = "Employee Status")]
        public string Status { get; set; } = "Active";

        public DateTime JoinedOn { get; set; } = DateTime.Now;
    }
}