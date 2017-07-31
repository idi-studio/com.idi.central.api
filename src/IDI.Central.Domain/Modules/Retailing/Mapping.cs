using IDI.Central.Domain.Modules.Retailing.AggregateRoots;
using IDI.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace IDI.Central.Domain.Modules.Retailing
{
    public class Mapping : EntityMapping
    {
        public override void Create(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasMany(e => e.Prices).WithOne(e=>e.Product).HasForeignKey(p => p.ProductId);
            modelBuilder.Entity<Product>().HasMany(e => e.Pictures).WithOne(e => e.Product).HasForeignKey(p => p.ProductId);
        }
    }
}
