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
            modelBuilder.Entity<Module>().HasMany(m => m.Privileges).WithOne(p => p.Module).HasForeignKey(p => p.ModuleId);
            modelBuilder.Entity<Menu>().HasOne(m => m.Module).WithMany(m => m.Menus).HasForeignKey(m => m.ModuleId);

            modelBuilder.Entity<RolePrivilege>().HasKey(rp => new { rp.RoleId, rp.PrivilegeId });
            modelBuilder.Entity<RolePrivilege>().HasOne(rp => rp.Role).WithMany(r => r.RolePrivileges).HasForeignKey(rp => rp.RoleId);
            modelBuilder.Entity<RolePrivilege>().HasOne(rp => rp.Privilege).WithMany(p => p.RolePrivileges).HasForeignKey(rp => rp.PrivilegeId);

            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<UserRole>().HasOne(ur => ur.User).WithMany(user => user.UserRoles).HasForeignKey(ur => ur.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserRole>().HasOne(ur => ur.Role).WithMany(role => role.UserRoles).HasForeignKey(ur => ur.RoleId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>().HasOne(u => u.Profile).WithOne().HasForeignKey<UserProfile>(p => p.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
