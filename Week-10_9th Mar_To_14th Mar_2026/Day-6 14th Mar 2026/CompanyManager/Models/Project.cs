using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required]
        public string Title { get; set; }

        public ICollection<EmployeeProject> EmployeeProjects { get; set; }
    }
}