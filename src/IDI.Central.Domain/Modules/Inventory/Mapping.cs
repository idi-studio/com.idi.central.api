using IDI.Central.Domain.Modules.Inventory.AggregateRoots;
using IDI.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace IDI.Central.Domain.Modules.Inventory
{
    public class Mapping : EntityMapping
    {
        public override void Create(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StoreTrans>().ToTable("StoreTrans");
            modelBuilder.Entity<Store>().HasMany(e => e.Stocks).WithOne(e => e.Store).HasForeignKey(e => e.StoreId);
        }
    }
}
