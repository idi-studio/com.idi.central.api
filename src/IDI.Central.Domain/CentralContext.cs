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
            new Modules.Administration.Mapping().Create(modelBuilder);
            new Modules.Material.Mapping().Create(modelBuilder);
            new Modules.Retailing.Mapping().Create(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
