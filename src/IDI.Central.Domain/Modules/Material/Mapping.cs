using IDI.Central.Domain.Modules.Material.AggregateRoots;
using IDI.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace IDI.Central.Domain.Modules.Material
{
    public class Mapping : EntityMapping
    {
        public override void Create(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<ProductPrice>().ToTable("ProductPrices");
            modelBuilder.Entity<ProductPicture>().ToTable("ProductPictures");

            //One-to-Many
            modelBuilder.Entity<Product>().HasMany(e => e.Prices).WithOne(e => e.Product).HasForeignKey(e => e.ProductId);
            modelBuilder.Entity<Product>().HasMany(e => e.Pictures).WithOne(e => e.Product).HasForeignKey(e => e.ProductId);
        }
    }
}
