using Microsoft.EntityFrameworkCore;
using E_Commerce_Application.Models;

namespace E_Commerce_Application.Data
{
	public class EcommerceContext : DbContext
	{
		public EcommerceContext(DbContextOptions<EcommerceContext> options)
			: base(options)
		{
		}

		public DbSet<Customer> Customers { get; set; }

		public DbSet<Product> Products { get; set; }

		public DbSet<Category> Categories { get; set; }

		public DbSet<Order> Orders { get; set; }

		public DbSet<OrderDetail> OrderDetails { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Decimal precision
			modelBuilder.Entity<Product>()
				.Property(p => p.Price)
				.HasColumnType("decimal(18,2)");

			modelBuilder.Entity<Order>()
				.Property(o => o.TotalAmount)
				.HasColumnType("decimal(18,2)");

			modelBuilder.Entity<OrderDetail>()
				.Property(o => o.UnitPrice)
				.HasColumnType("decimal(18,2)");

			// Category → Products
			modelBuilder.Entity<Category>()
				.HasMany(c => c.Products)
				.WithOne(p => p.Category)
				.HasForeignKey(p => p.CategoryId);

			// Customer → Orders
			modelBuilder.Entity<Customer>()
				.HasMany(c => c.Orders)
				.WithOne(o => o.Customer)
				.HasForeignKey(o => o.CustomerId);

			// Order → OrderDetails
			modelBuilder.Entity<Order>()
				.HasMany(o => o.OrderDetails)
				.WithOne(od => od.Order)
				.HasForeignKey(od => od.OrderId);

			// Product → OrderDetails
			modelBuilder.Entity<Product>()
				.HasMany(p => p.OrderDetails)
				.WithOne(od => od.Product)
				.HasForeignKey(od => od.ProductId);
		}
	}
}