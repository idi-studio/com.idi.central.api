﻿using System;
using System.Linq;
using IDI.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace IDI.Core.Repositories
{
    public class RepositoryContext : IRepositoryContext
    {
        private readonly Guid id = Guid.NewGuid();
        private readonly DbContext context;
        private volatile bool committed = true;
        private readonly object sync = new object();

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
                    affected = context.SaveChanges();
                }
                Committed = true;
            }

            return affected;
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

        public void Add<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot
        {
            context.Add(aggregateRoot);
            this.Committed = false;
        }
        public void Update<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot
        {
            context.Update(aggregateRoot);
            this.Committed = false;
        }
        public void Remove<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot
        {
            context.Remove(aggregateRoot);
            this.Committed = false;
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

        ~RepositoryContext()
        {
            this.Dispose(false);
        }
    }
}
