using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using OrderService.Domain.ValueObjects;

namespace OrderService.Infrastructure.Persistence
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OrderNumber).IsRequired();
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.OrderDate).IsRequired();
                
                entity.OwnsOne(e => e.ShippingAddress, address =>
                {
                    address.Property(a => a.Street).IsRequired();
                    address.Property(a => a.City).IsRequired();
                    address.Property(a => a.State).IsRequired();
                    address.Property(a => a.Country).IsRequired();
                    address.Property(a => a.ZipCode).IsRequired();
                });

                entity.HasMany(e => e.OrderItems)
                    .WithOne()
                    .HasForeignKey("OrderId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ProductName).IsRequired();
                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18,2)");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
} 