using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using IDI.Core.Authentication;
using IDI.Core.Domain;

namespace IDI.Core.Repositories
{
    public class Repository<TAggregateRoot> : IRepository<TAggregateRoot> where TAggregateRoot : AggregateRoot
    {
        private readonly IRepositoryContext context;
        private readonly ICurrentUser user;

        public IQueryable<TAggregateRoot> Source { get { return context.Source<TAggregateRoot>(); } }

        public Repository(IRepositoryContext context, ICurrentUser user)
        {
            this.context = context;
            this.user = user;
        }

        #region IRepository<TAggregateRoot> Members
        public void Add(TAggregateRoot aggregateRoot)
        {
            if (user != null && user.IsAuthenticated)
            {
                aggregateRoot.CreatedBy = user.Name;
                aggregateRoot.LastUpdatedBy = user.Name;
            }
            context.Add(aggregateRoot);
        }
        public void Remove(TAggregateRoot aggregateRoot)
        {
            context.Remove(aggregateRoot);
        }
        public void Update(TAggregateRoot aggregateRoot)
        {
            if (user != null && user.IsAuthenticated)
            {
                aggregateRoot.LastUpdatedAt = DateTime.Now;
                aggregateRoot.LastUpdatedBy = user.Name;
            }
            context.Update(aggregateRoot);
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
