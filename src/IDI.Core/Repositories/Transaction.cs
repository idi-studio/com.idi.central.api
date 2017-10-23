using System;
using System.Collections.Generic;
using IDI.Core.Authentication;
using IDI.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace IDI.Core.Repositories
{
    public class Transaction : ITransaction
    {
        private readonly IRepositoryContext context;
        private readonly ICurrentUser user;
        private IDbContextTransaction transaction = null;

        public Guid TransactionId => transaction.TransactionId;

        public Transaction(IRepositoryContext context, ICurrentUser user)
        {
            this.user = user;
            this.context = context;
            this.context.BeforeCommitted = (entity, state, timestamp) =>
            {
                var name = "anonymous";

                if (user != null && user.IsAuthenticated)
                    name = user.Name;

                switch (state)
                {
                    case EntityState.Added:
                        entity.TransactionId = this.TransactionId;
                        entity.CreatedBy = user.Name;
                        entity.CreatedAt = timestamp;
                        entity.LastUpdatedBy = user.Name;
                        entity.LastUpdatedAt = timestamp;
                        break;
                    case EntityState.Modified:
                        entity.TransactionId = this.TransactionId;
                        entity.LastUpdatedBy = user.Name;
                        entity.LastUpdatedAt = timestamp;
                        break;
                    default:
                        break;
                }
            };
        }

        public ITransaction Begin()
        {
            this.transaction = this.context.BeginTransaction();

            return this;
        }

        public bool EnsureCreated()
        {
            return context.EnsureCreated();
        }

        public void Dispose()
        {
            this.context.Dispose();
            this.transaction.Dispose();
        }

        #region ITransaction Members
        public void Add<TAggregateRoot>(TAggregateRoot arg) where TAggregateRoot : AggregateRoot
        {
            this.context.Add(arg);
        }

        public void AddRange<TAggregateRoot>(List<TAggregateRoot> args) where TAggregateRoot : AggregateRoot
        {
            this.context.AddRange(args);
        }

        public void Update<TAggregateRoot>(TAggregateRoot arg) where TAggregateRoot : AggregateRoot
        {
            this.context.Update(arg);
        }

        public void Remove<TAggregateRoot>(TAggregateRoot arg) where TAggregateRoot : AggregateRoot
        {
            context.Remove(arg);
        }

        public void Commit()
        {
            this.context.Commit();
            this.transaction.Commit();
        }

        public void Rollback()
        {
            this.transaction.Rollback();
        }

        public IQueryableRepository<TAggregateRoot> Source<TAggregateRoot>() where TAggregateRoot : AggregateRoot
        {
            return new Repository<TAggregateRoot>(this.context, this.user);
        }
        #endregion
    }
}
