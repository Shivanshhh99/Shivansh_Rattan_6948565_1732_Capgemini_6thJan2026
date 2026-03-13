using Microsoft.EntityFrameworkCore;

namespace CompanyEmployees.Models
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
             : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<EmployeeModel> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeModel>()
                .HasOne(e => e.Company)
                .WithMany(c => c.employees)
                .HasForeignKey(e => e.CompanyId);
        }
    }
}
