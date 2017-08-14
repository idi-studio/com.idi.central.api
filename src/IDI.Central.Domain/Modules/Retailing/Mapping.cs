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
            modelBuilder.Entity<Product>().HasMany(e => e.Prices).WithOne(e => e.Product).HasForeignKey(e => e.ProductId);
        }
    }
}
