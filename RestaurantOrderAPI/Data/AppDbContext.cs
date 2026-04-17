//using Microsoft.EntityFrameworkCore;
//using RestaurantOrderAPI.Models;

//namespace RestaurantOrderAPI.Data;

//public class AppDbContext : DbContext
//{
//    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

//    public DbSet<User> Users { get; set; }
//    public DbSet<Product> Products { get; set; }
//    public DbSet<Order> Orders { get; set; }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        base.OnModelCreating(modelBuilder);

//        modelBuilder.Entity<Product>()
//            .Property(p => p.Price)
//            .HasPrecision(10, 2);

//        modelBuilder.Entity<Order>()
//            .Property(o => o.Total)
//            .HasPrecision(10, 2);

//        modelBuilder.Entity<Order>()
//        .HasMany(o => o.Items)
//        .WithOne(i => i.Order)
//        .HasForeignKey(i => i.OrderId)
//        .OnDelete(DeleteBehavior.Cascade);

//        modelBuilder.Entity<OrderItem>()
//            .HasOne(i => i.Product)
//            .WithMany(p => p.OrderItems)
//            .HasForeignKey(i => i.ProductId)
//            .OnDelete(DeleteBehavior.Restrict);
//    }

//}
