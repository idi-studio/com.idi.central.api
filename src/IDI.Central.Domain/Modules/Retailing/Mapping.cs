using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace IDI.Central.Domain.Modules.Retailing
{
    public class Mapping : EntityMapping
    {
        public override void Create(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<ProductPrice>().ToTable("ProductPrices");
            modelBuilder.Entity<ProductPicture>().ToTable("ProductPictures");
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<OrderItem>().ToTable("OrderItems");
            modelBuilder.Entity<Voucher>().ToTable("Vouchers");
            modelBuilder.Entity<ShippingAddress>().ToTable("Addresses");

            //One-to-one
            modelBuilder.Entity<Customer>().HasOne(e => e.User).WithOne().HasForeignKey<Customer>(e => e.UserId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Voucher>().HasOne(e => e.Order).WithOne().HasForeignKey<Voucher>(e => e.OrderId).IsRequired();

            //One-to-Many
            modelBuilder.Entity<Product>().HasMany(e => e.Prices).WithOne(e => e.Product).HasForeignKey(e => e.ProductId);
            modelBuilder.Entity<Product>().HasMany(e => e.Pictures).WithOne(e => e.Product).HasForeignKey(e => e.ProductId);
            modelBuilder.Entity<Order>().HasMany(e => e.Items).WithOne(e => e.Order).HasForeignKey(e => e.OrderId);
            modelBuilder.Entity<Customer>().HasMany(e => e.Orders).WithOne(e => e.Customer).HasForeignKey(e => e.CustomerId).IsRequired(false);
        }
    }
}
