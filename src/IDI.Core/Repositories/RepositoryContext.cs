using System;
using System.Collections.Generic;
using System.Linq;
using IDI.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace IDI.Core.Repositories
{
    public class RepositoryContext : IRepositoryContext
    {
        private readonly Guid id = Guid.NewGuid();
        private readonly DbContext context;
        private volatile bool committed = true;
        private readonly object sync = new object();

        public Action<AggregateRoot, EntityState, DateTime> BeforeCommitted { get; set; }

        public RepositoryContext(DbContext context)
        {
            this.context = context;
        }

        #region IUnitOfWork Members
        public bool Committed
        {
            get { return committed; }
            protected set { committed = value; }
        }
        public int Commit()
        {
            int affected = 0;

            if (!Committed)
            {
                lock (sync)
                {
                    BeforeCommittedHandle();
                    affected = context.SaveChanges();
                }
                Committed = true;
            }

            return affected;
        }

        private void BeforeCommittedHandle()
        {
            if (this.BeforeCommitted == null)
                return;

            var timestamp = DateTime.Now;
            var entries = this.context.ChangeTracker.Entries<AggregateRoot>();

            foreach (var entry in entries)
            {
                this.BeforeCommitted(entry.Entity, entry.State, timestamp);
            }
        }

        public void Rollback()
        {
            Committed = false;
        }
        #endregion

        #region IRepositoryContext Members
        public Guid Id { get { return id; } }

        public IQueryable<TAggregateRoot> Source<TAggregateRoot>() where TAggregateRoot : AggregateRoot
        {
            return context.Set<TAggregateRoot>();
        }

        public void Add<TAggregateRoot>(TAggregateRoot arg) where TAggregateRoot : AggregateRoot
        {
            context.Add(arg);
            this.Committed = false;
        }
        public void AddRange<TAggregateRoot>(List<TAggregateRoot> args) where TAggregateRoot : AggregateRoot
        {
            context.AddRange(args);
            this.Committed = false;
        }
        public void Update<TAggregateRoot>(TAggregateRoot arg) where TAggregateRoot : AggregateRoot
        {
            context.Update(arg);
            this.Committed = false;
        }
        public void UpdateRange<TAggregateRoot>(List<TAggregateRoot> args) where TAggregateRoot : AggregateRoot
        {
            context.UpdateRange(args);
            this.Committed = false;
        }
        public void Remove<TAggregateRoot>(TAggregateRoot arg) where TAggregateRoot : AggregateRoot
        {
            context.Remove(arg);
            this.Committed = false;
        }

        public bool EnsureCreated()
        {
            return context.Database.EnsureCreated();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
        #endregion

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IDbContextTransaction BeginTransaction()
        {
            return this.context.Database.BeginTransaction();
        }

        ~RepositoryContext()
        {
            this.Dispose(false);
        }
    }
}
