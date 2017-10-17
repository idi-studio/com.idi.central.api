using System;
using System.Collections.Generic;
using IDI.Core.Authentication;
using IDI.Core.Domain;
using Microsoft.EntityFrameworkCore.Storage;

namespace IDI.Core.Repositories
{
    public class Transaction : ITransaction
    {
        private readonly IRepositoryContext context;
        private readonly ICurrentUser user;
        private IDbContextTransaction transaction = null;

        public Transaction(IRepositoryContext context, ICurrentUser user)
        {
            this.context = context;
            this.user = user;
        }

        public Guid TransactionId => transaction.TransactionId;

        public ITransaction Begin()
        {
            this.transaction = this.context.BeginTransaction();

            return this;
        }

        public void Dispose()
        {
            this.context.Dispose();
            this.transaction.Dispose();
        }

        #region ITransaction Members
        public void Add<TAggregateRoot>(TAggregateRoot arg) where TAggregateRoot : AggregateRoot
        {
            if (user != null && user.IsAuthenticated)
            {
                arg.CreatedBy = user.Name;
                arg.LastUpdatedBy = user.Name;
            }
            this.context.Add(arg);
        }

        public void AddRange<TAggregateRoot>(List<TAggregateRoot> args) where TAggregateRoot : AggregateRoot
        {
            if (user != null && user.IsAuthenticated)
            {
                args.ForEach(arg =>
                {
                    arg.CreatedBy = user.Name;
                    arg.LastUpdatedBy = user.Name;
                });
            }

            this.context.AddRange(args);
        }

        public void Update<TAggregateRoot>(TAggregateRoot arg) where TAggregateRoot : AggregateRoot
        {
            if (user != null && user.IsAuthenticated)
            {
                arg.LastUpdatedAt = DateTime.Now;
                arg.LastUpdatedBy = user.Name;
            }
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
