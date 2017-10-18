using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using IDI.Core.Authentication;
using IDI.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace IDI.Core.Repositories
{
    public class Repository<TAggregateRoot> : IRepository<TAggregateRoot> where TAggregateRoot : AggregateRoot
    {
        private readonly IRepositoryContext context;
        private readonly ICurrentUser user;

        public IQueryable<TAggregateRoot> Source => context.Source<TAggregateRoot>();

        public Repository(IRepositoryContext context, ICurrentUser user)
        {
            this.user = user;
            this.context = context;
            this.context.BeforeCommitted = (entity, state) =>
            {
                if (!(user != null && user.IsAuthenticated))
                    return;

                if (state == EntityState.Added)
                {
                    entity.CreatedBy = user.Name;
                    entity.LastUpdatedBy = user.Name;
                    entity.TransactionId = context.Id;
                }

                if (state == EntityState.Modified)
                {
                    entity.LastUpdatedAt = DateTime.Now;
                    entity.LastUpdatedBy = user.Name;
                    entity.TransactionId = context.Id;
                }
            };
        }

        #region IRepository<TAggregateRoot> Members
        public void Add(TAggregateRoot arg)
        {
            context.Add(arg);
        }
        public void AddRange(List<TAggregateRoot> args)
        {
            context.AddRange(args);
        }
        public void Remove(TAggregateRoot arg)
        {
            context.Remove(arg);
        }
        public void Update(TAggregateRoot arg)
        {
            context.Update(arg);
        }
        public int Commit()
        {
            return context.Commit();
        }
        #endregion

        #region IQueryableRepository<TAggregateRoot> Members
        public bool Exist(Expression<Func<TAggregateRoot, bool>> condition)
        {
            return Source.Any(condition);
        }
        public int Count(Expression<Func<TAggregateRoot, bool>> condition)
        {
            return Source.Count(condition);
        }
        public TAggregateRoot Find(Guid key)
        {
            return Source.FirstOrDefault(e => e.Id == key);
        }
        public TAggregateRoot Find(Expression<Func<TAggregateRoot, bool>> condition)
        {
            return Source.FirstOrDefault(condition);
        }
        public List<TAggregateRoot> Get()
        {
            return Source.ToList();
        }
        public List<TAggregateRoot> Get(Expression<Func<TAggregateRoot, bool>> condition)
        {
            return Source.Where(condition).ToList();
        }
        #endregion
    }
}
