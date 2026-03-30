using ECommerceOMS.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceOMS.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShippingDetail> ShippingDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ── Customer → Orders (One-to-Many) ──────────────────────────
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // ── Order → OrderItems (One-to-Many) ─────────────────────────
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // ── Product → OrderItems (Many-to-Many via OrderItem) ─────────
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Category → Products (One-to-Many) ────────────────────────
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Order → ShippingDetail (One-to-One) ───────────────────────
            modelBuilder.Entity<ShippingDetail>()
                .HasOne(sd => sd.Order)
                .WithOne(o => o.ShippingDetail)
                .HasForeignKey<ShippingDetail>(sd => sd.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // ── Decimal precision ─────────────────────────────────────────
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.UnitPrice)
                .HasColumnType("decimal(18,2)");

            // ── Seed Data ─────────────────────────────────────────────────
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics", Description = "Gadgets and devices" },
                new Category { Id = 2, Name = "Clothing", Description = "Apparel and accessories" },
                new Category { Id = 3, Name = "Books", Description = "All kinds of books" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Description = "15-inch laptop", Price = 75000, StockQuantity = 50, CategoryId = 1 },
                new Product { Id = 2, Name = "Smartphone", Description = "Android phone", Price = 25000, StockQuantity = 100, CategoryId = 1 },
                new Product { Id = 3, Name = "T-Shirt", Description = "Cotton t-shirt", Price = 499, StockQuantity = 200, CategoryId = 2 },
                new Product { Id = 4, Name = "ASP.NET Core Book", Description = "Learn ASP.NET Core", Price = 799, StockQuantity = 75, CategoryId = 3 }
            );
        }
    }
}