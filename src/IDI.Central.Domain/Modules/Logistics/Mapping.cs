using IDI.Central.Domain.Modules.Logistics.AggregateRoots;
using IDI.Central.Domain.Modules.Sales.AggregateRoots;
using IDI.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace IDI.Central.Domain.Modules.Logistics
{
    public class Mapping : EntityMapping
    {
        public override void Create(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Deliver>().ToTable("Delivers");

            //One-to-Many
            modelBuilder.Entity<Deliver>().HasOne(e => e.Order).WithOne().HasForeignKey<Voucher>(e => e.OrderId).IsRequired();
        }
    }
}
