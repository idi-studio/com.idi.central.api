using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace IDI.Central.Domain.Modules.Retailing
{
    public class Mapping : EntityMapping
    {
        public override void Create(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<OrderItem>().ToTable("OrderItems");
            modelBuilder.Entity<Voucher>().ToTable("Vouchers");
            modelBuilder.Entity<ShippingAddress>().ToTable("Addresses");

            //One-to-one
            modelBuilder.Entity<Customer>().HasOne(e => e.User).WithOne().HasForeignKey<Customer>(e => e.UserId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Voucher>().HasOne(e => e.Order).WithOne().HasForeignKey<Voucher>(e => e.OrderId).IsRequired();

            //One-to-Many
            modelBuilder.Entity<Order>().HasMany(e => e.Items).WithOne(e => e.Order).HasForeignKey(e => e.OrderId);
            modelBuilder.Entity<Customer>().HasMany(e => e.Orders).WithOne(e => e.Customer).HasForeignKey(e => e.CustomerId).IsRequired(false);
            modelBuilder.Entity<Customer>().HasMany(e => e.Shippings).WithOne(e => e.Customer).HasForeignKey(e => e.CustomerId).IsRequired();
        }
    }
}
