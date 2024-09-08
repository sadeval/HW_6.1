using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FlowerShopManagement
{
    public class City
    {
        public int CityId { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<Shop> Shops { get; set; } = new List<Shop>();
    }

    public class Shop
    {
        public int ShopId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int CityId { get; set; }
        public virtual City? City { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<ShopSupplier> ShopSuppliers { get; set; } = new List<ShopSupplier>();
    }

    public class Supplier
    {
        public int SupplierId { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<ShopSupplier> ShopSuppliers { get; set; } = new List<ShopSupplier>();
    }

    public class ShopSupplier
    {
        public int ShopId { get; set; }
        public Shop? Shop { get; set; }
        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
    }

    public class Product
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int ShopId { get; set; }
        public virtual Shop? Shop { get; set; }
        public int Quantity { get; set; }
    }

    public class FlowerShopContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=FlowerShopDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShopSupplier>()
                .HasKey(ss => new { ss.ShopId, ss.SupplierId });

            modelBuilder.Entity<ShopSupplier>()
                .HasOne(ss => ss.Shop)
                .WithMany(s => s.ShopSuppliers)
                .HasForeignKey(ss => ss.ShopId);

            modelBuilder.Entity<ShopSupplier>()
                .HasOne(ss => ss.Supplier)
                .WithMany(s => s.ShopSuppliers)
                .HasForeignKey(ss => ss.SupplierId);
        }
    }
}
