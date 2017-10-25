using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace IDI.Central.Domain.Modules.Administration
{
    public class Mapping : EntityMapping
    {
        public override void Create(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOne(e => e.Profile).WithOne().HasForeignKey<UserProfile>(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasOne(e => e.Role).WithOne().HasForeignKey<UserRole>(e => e.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Module>().HasMany(e => e.Menus).WithOne(e => e.Module).HasForeignKey(e => e.ModuleId);
        }
    }
}
