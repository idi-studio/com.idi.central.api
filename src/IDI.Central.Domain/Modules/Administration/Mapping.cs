using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace IDI.Central.Domain.Modules.Administration
{
    public class Mapping : EntityMapping
    {
        public override void Create(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>();
            modelBuilder.Entity<Role>();
            modelBuilder.Entity<Permission>();
            modelBuilder.Entity<User>().HasOne(u => u.Profile).WithOne().HasForeignKey<UserProfile>(p => p.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasOne(u => u.Role).WithOne().HasForeignKey<UserRole>(p => p.UserId).OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<Module>().HasMany(m => m.Permissions).WithOne(p => p.Module).HasForeignKey(p => p.ModuleId);
            //modelBuilder.Entity<Menu>().HasOne(m => m.Module).WithMany(m => m.Menus).HasForeignKey(m => m.ModuleId);
        }
    }
}
