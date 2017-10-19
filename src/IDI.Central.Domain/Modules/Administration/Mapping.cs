using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace IDI.Central.Domain.Modules.Administration
{
    public class Mapping : EntityMapping
    {
        public override void Create(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOne(u => u.Profile).WithOne().HasForeignKey<UserProfile>(p => p.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasOne(u => u.Role).WithOne().HasForeignKey<UserRole>(p => p.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
