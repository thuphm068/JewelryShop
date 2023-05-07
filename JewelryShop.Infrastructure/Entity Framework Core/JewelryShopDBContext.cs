using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using JewelryShop.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Metrics;


namespace JewelryShop.Infrastructure.Entity_Framework_Core;

public class JewelryShopDBContext : DbContext
{
    public DbSet<Product> _products { get; set; } = null;
    public DbSet<Customer> _customers { get; set; } = null;
    public DbSet<Cart> _carts { get; set; } = null;
    public DbSet<CartItem> _cartitems { get; set; } = null;
    public DbSet<Order> _orders { get; set; } = null;
    public DbSet<OrderDetail> _orderdetails { get; set; } = null;
    public DbSet<Category> _categories { get; set; } = null;
    public DbSet<SubCategory> _subcategories { get; set; } = null;

    public DbSet<Warranty> _warrantys { get; set; } = null;

    public JewelryShopDBContext(DbContextOptions<JewelryShopDBContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Warranty>(re =>
        {
            re.ToTable("Warranty");
            re.HasKey(x => x.Id);
            re.Property(r => r.Period).IsRequired();
            re.Property(r => r.Description).IsRequired();
        });

        modelBuilder.Entity<Product>(re =>
        {
            re.ToTable("Product");
            re.HasKey(r => r.Id);
            re.Property(r => r.Name).IsRequired().HasMaxLength(100);
            re.Property(r => r.Price).IsRequired();
            re.Property(r => r.Description).IsRequired();
            re.Property(r => r.Image).IsRequired();
            re.Property(r => r.Material).IsRequired().HasMaxLength(20);
            re.HasOne<Warranty>().WithMany().HasForeignKey(x => x.WarrantyId).IsRequired(false);
            re.HasOne<SubCategory>().WithMany().HasForeignKey(x => x.SubCategoryId).IsRequired();
        });
        modelBuilder.Entity<Customer>(re =>
        {
            re.ToTable("Customer");
            re.HasKey(r => r.Id);
            re.Property(r => r.Name).IsRequired().HasMaxLength(50);
            re.Property(r => r.Address).IsRequired().HasMaxLength(200);
            re.Property(r => r.Email).IsRequired().HasMaxLength(50);
            re.Property(r => r.Password).IsRequired().HasMaxLength(20);
            re.Property(r => r.Gender).IsRequired();
            re.Property(r => r.Birthday).IsRequired();
            re.Property(r => r.Phone).IsRequired().HasMaxLength(10);
        });

        modelBuilder.Entity<Cart>(re =>
        {
            re.ToTable("Cart");
            re.HasKey(r => r.Id);
            re.Property(r => r.Total).IsRequired();
            re.HasOne<Customer>().WithOne().HasForeignKey<Cart>(r => r.CustomerId).IsRequired();//test
        });



        modelBuilder.Entity<CartItem>(re =>
        {
            re.ToTable("CartItem");
            re.HasKey(r => r.Id);
            re.Property(r => r.TotalPrice).IsRequired();
            re.Property(r => r.Quantity).IsRequired();
            re.HasOne<Cart>().WithMany().HasForeignKey(x => x.CartId).IsRequired();
            re.HasOne<Product>().WithMany().HasForeignKey(x => x.ProductId).IsRequired();


        });

        modelBuilder.Entity<Order>(re =>
        {
            re.ToTable("Order");
            re.HasKey(r => r.Id);
            re.Property(r => r.Status).IsRequired();
            re.Property(r => r.Total).IsRequired();
            re.Property(r => r.Date).IsRequired();
            re.HasOne<Customer>().WithMany().HasForeignKey(x => x.CustomerId).IsRequired();
        });


        modelBuilder.Entity<OrderDetail>(re =>
        {
            re.ToTable("OrderDetail");
            re.HasKey(r => r.Id);
            re.Property(r => r.Quantity).IsRequired();
            re.Property(r => r.TotalPrice).IsRequired();
            re.HasOne<Order>().WithMany().HasForeignKey(x => x.OrderId).IsRequired();
            re.HasOne<Product>().WithMany().HasForeignKey(x => x.ProductId).IsRequired();
        });
        modelBuilder.Entity<Category>(re =>
        {
            re.ToTable("Category");
            re.HasKey(r => r.Id);
            re.Property(r => r.Name).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<SubCategory>(re =>
        {
            re.ToTable("SubCategory");
            re.HasKey(r => r.Id);
            re.Property(r => r.Name).IsRequired().HasMaxLength(50);
            re.HasOne<Category>().WithMany().HasForeignKey(x => x.CategoryId).IsRequired();

        });


    }



}

public class JewelryShopContextFactory : IDesignTimeDbContextFactory<JewelryShopDBContext>
{
public JewelryShopDBContext CreateDbContext(string[] args)
{
    var optionsBuilder = new DbContextOptionsBuilder<JewelryShopDBContext>();
    optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=JewelryShop;Trusted_Connection=True;TrustServerCertificate=True");
    return new JewelryShopDBContext(optionsBuilder.Options);
}
}
