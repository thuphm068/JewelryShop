using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using JewelryShop.Domain.Entities;


namespace JewelryShop.Infrastructure.Entity_Framework_Core
{
    public class JewelryShopDBContext:DbContext
    {
        public DbSet<Product> _products { get; set; } = null;
        public DbSet<Customer> _customers { get; set; } = null;
        public DbSet<Cart> _carts { get; set; } = null;
        public DbSet<CartItem> _cartitems { get; set; } = null;
        public DbSet<Order> _orders { get; set; } = null;
        public DbSet<OrderDetail> _orderdetails { get; set; } = null;
        public DbSet<Category> _categories { get; set; } = null;
        public DbSet<Warranty> _warrantys { get; set; } = null;

        public JewelryShopDBContext(DbContextOptions<JewelryShopDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>(re =>
            {
                re.ToTable("Product");
                re.HasKey(r => r.Id);
                re.Property(r => r.Name).IsRequired().HasMaxLength(128);
                re.Property(r => r.Price).IsRequired();
                re.Property(r => r.Description).IsRequired().HasMaxLength(5000);
                re.HasOne<Warranty>().WithOne().HasForeignKey<Warranty>(x => x.Id).IsRequired();                //re.Property(r => r.WarrantyId);
                re.Property(r => r.CategoryId).IsRequired();

            });
            modelBuilder.Entity<Customer>(re =>
            {
                re.ToTable("Customer");
                re.HasKey(r => r.Id);
                re.Property(r => r.Name).IsRequired();
                re.Property(r => r.Address).IsRequired().HasMaxLength(300);
                re.Property(r => r.Email).IsRequired().HasMaxLength(320);
                re.Property(r => r.Password).IsRequired();
                re.Property(r => r.Gender).IsRequired();
                re.Property(r => r.Birthday);
                re.Property(r => r.Phone).IsRequired();
            });

            modelBuilder.Entity<Cart>()
                .ToTable("Cart")
                .HasKey(r => r.Id);

            modelBuilder.Entity<CartItem>()
                .ToTable("CartItem")
                .HasKey(r => r.Id);
            modelBuilder.Entity<Order>()
                .ToTable("Order")
                .HasKey(r => r.Id);
            modelBuilder.Entity<OrderDetail>().ToTable("OrderDetail");
            modelBuilder.Entity<Category>().ToTable("Category");

        }

    }
}
