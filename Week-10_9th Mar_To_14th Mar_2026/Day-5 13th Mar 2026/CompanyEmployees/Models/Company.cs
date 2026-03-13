using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyEmployees.Models
{
    [Table("tblCompany")]
    public class Company
    {
        [Key]
        public int  CompanyId { get; set; }
        public string CompanyName {  get; set; }
        public string Location { get; set; }

        public List<EmployeeModel> employees {  get; set; }
        

    }
}
