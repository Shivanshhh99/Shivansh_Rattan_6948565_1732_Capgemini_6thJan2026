using CompanyEmployees.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CompanyEmployees
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<CompanyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CompanyDB")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CompanyDbContext>();

                if (!context.Companies.Any())
                {
                    var c1 = new Company
                    {
                        CompanyName = "Microsoft",
                        Location = "USA",
                        employees = new List<EmployeeModel>
            {
                new EmployeeModel { Name = "John", Designation = "Developer" },
                new EmployeeModel{ Name = "Sara", Designation = "Manager" }
            }
                    };

                    var c2 = new Company
                    {
                        CompanyName = "Google",
                        Location = "USA",
                        employees = new List<EmployeeModel>
            {
                new EmployeeModel { Name = "Alex", Designation = "Engineer" },
                new EmployeeModel { Name = "Emma", Designation = "Designer" }
            }
                    };

                    context.Companies.AddRange(c1, c2);
                    context.SaveChanges();
                }
            }

            app.Run();
        }
    }
}
