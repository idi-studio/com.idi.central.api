using IDI.Central.Domain.Modules.BasicInfo.AggregateRoots;
using IDI.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace IDI.Central.Domain.Modules.BasicInfo
{
    public class Mapping : EntityMapping
    {
        public override void Create(ModelBuilder modelBuilder)
        {
            //One-to-Many
            modelBuilder.Entity<Product>().HasMany(e => e.Prices).WithOne(e => e.Product).HasForeignKey(e => e.ProductId);
            modelBuilder.Entity<Product>().HasMany(e => e.Pictures).WithOne(e => e.Product).HasForeignKey(e => e.ProductId);
            modelBuilder.Entity<Product>().HasMany(e => e.Stocks).WithOne(e => e.Product).HasForeignKey(e => e.ProductId);

            modelBuilder.Entity<Product>().HasOne(e => e.Stock).WithOne().HasForeignKey<ProductStock>(e => e.ProductId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>().HasOne(e => e.Article).WithOne().HasForeignKey<ProductArticle>(e => e.ProductId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
