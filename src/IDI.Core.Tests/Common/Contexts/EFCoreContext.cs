using IDI.Core.Tests.Common.AggregateRoots;
using Microsoft.EntityFrameworkCore;

namespace IDI.Core.Tests.Common.Contexts
{
    public class EFCoreContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public EFCoreContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
            modelBuilder.Entity<Blog>().HasMany(blog => blog.Posts).WithOne(post => post.Blog).HasForeignKey(post => post.BlogId);
            modelBuilder.Entity<Post>().HasOne(post => post.Blog).WithMany(blog => blog.Posts).HasForeignKey(post => post.BlogId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
