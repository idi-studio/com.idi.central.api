using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using IDI.Core.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;

namespace IDI.Core.Domain
{
    public abstract class DomainContext : DbContext
    {
        public DomainContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Initialize(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void Initialize(ModelBuilder modelBuilder)
        {
            var types = this.GetType().Assembly.GetTypes();

            types.Where(t => t.BaseType == typeof(AggregateRoot) || t.BaseType.BaseType == typeof(AggregateRoot)).Where(e=>e.HasAttribute<TableAttribute>()).ToList().ForEach(t => modelBuilder.Entity(t));
            types.Where(t => t.BaseType == typeof(EntityMapping)).ToList().ForEach(t => ((EntityMapping)Activator.CreateInstance(t)).Create(modelBuilder));
        }
    }
}
