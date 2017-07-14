using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using Microsoft.EntityFrameworkCore;

namespace IDI.Central.Domain
{
    public class CentralContext : DbContext
    {
        #region Administration
        public DbSet<Module> Modules { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Privilege> Privileges { get; set; }
        public DbSet<RolePrivilege> RolePrivileges { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        #endregion

        public CentralContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Administration
            modelBuilder.Entity<Module>().HasMany(m => m.Privileges).WithOne(p => p.Module).HasForeignKey(p => p.ModuleId);
            modelBuilder.Entity<Menu>().HasOne(m => m.Module).WithMany(m => m.Menus).HasForeignKey(m => m.ModuleId);
            modelBuilder.Entity<RolePrivilege>().HasKey(rp => new { rp.RoleId, rp.PrivilegeId });
            modelBuilder.Entity<RolePrivilege>().HasOne(rp => rp.Role).WithMany(r => r.RolePrivileges).HasForeignKey(rp => rp.RoleId);
            modelBuilder.Entity<RolePrivilege>().HasOne(rp => rp.Privilege).WithMany(p => p.RolePrivileges).HasForeignKey(rp => rp.PrivilegeId);
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<UserRole>().HasOne(ur => ur.User).WithMany(user => user.UserRoles).HasForeignKey(ur => ur.UserId);
            modelBuilder.Entity<UserRole>().HasOne(ur => ur.Role).WithMany(role => role.UserRoles).HasForeignKey(ur => ur.RoleId);
            modelBuilder.Entity<User>().HasOne(u => u.Profile).WithOne(p => p.User).HasForeignKey<UserProfile>(p => p.UserId);
            modelBuilder.Entity<Client>();
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
