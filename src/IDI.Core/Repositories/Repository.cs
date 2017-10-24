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
            this.context.BeforeCommitted = (entity, state, timestamp) =>
            {
                var name = "anonymous";

                if (user != null && user.IsAuthenticated)
                    name = user.Name;

                switch (state)
                {
                    case EntityState.Added:
                        entity.TransactionId = context.Id;
                        entity.CreatedBy = user.Name;
                        entity.CreatedAt = timestamp;
                        entity.LastUpdatedBy = user.Name;
                        entity.LastUpdatedAt = timestamp;
                        break;
                    case EntityState.Modified:
                        entity.TransactionId = context.Id;
                        entity.LastUpdatedBy = user.Name;
                        entity.LastUpdatedAt = timestamp;
                        break;
                    default:
                        break;
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
        public void UpdateRange(List<TAggregateRoot> args)
        {
            context.UpdateRange(args);
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
