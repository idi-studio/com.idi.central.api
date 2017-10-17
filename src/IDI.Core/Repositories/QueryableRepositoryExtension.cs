using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using IDI.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace IDI.Core.Repositories
{
    internal class IncludableQueryableRepository<TAggregateRoot, TProperty> : IIncludableQueryableRepository<TAggregateRoot, TProperty>, IAsyncEnumerable<TAggregateRoot> where TAggregateRoot : AggregateRoot
    {
        private readonly IQueryable<TAggregateRoot> queryable;

        #region IQueryable<TAggregateRoot> Members

        public Expression Expression => this.queryable.Expression;

        public Type ElementType => this.queryable.ElementType;

        public IQueryProvider Provider => this.queryable.Provider;

        public IQueryable<TAggregateRoot> Source => this.queryable;

        public IncludableQueryableRepository(IQueryable<TAggregateRoot> queryable)
        {
            this.queryable = queryable;
        }

        public IEnumerator<TAggregateRoot> GetEnumerator()
        {
            return this.queryable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        IAsyncEnumerator<TAggregateRoot> IAsyncEnumerable<TAggregateRoot>.GetEnumerator()
        {
            return ((IAsyncEnumerable<TAggregateRoot>)this.queryable).GetEnumerator();
        }

        #endregion

        #region IQueryableRepository<TAggregateRoot> Members 
        public bool Exist(Expression<Func<TAggregateRoot, bool>> condition)
        {
            return queryable.Any(condition);
        }

        public int Count(Expression<Func<TAggregateRoot, bool>> condition)
        {
            return queryable.Count(condition);
        }

        public TAggregateRoot Find(Guid key)
        {
            return queryable.FirstOrDefault(e => e.Id == key);
        }

        public TAggregateRoot Find(Expression<Func<TAggregateRoot, bool>> condition)
        {
            return queryable.FirstOrDefault(condition);
        }

        public List<TAggregateRoot> Get()
        {
            return queryable.ToList();
        }

        public List<TAggregateRoot> Get(Expression<Func<TAggregateRoot, bool>> condition)
        {
            return queryable.Where(condition).ToList();
        }
        #endregion
    }

    public static class QueryableRepositoryExtension
    {
        public static IIncludableQueryableRepository<TAggregateRoot, TProperty> Include<TAggregateRoot, TProperty>(this IQueryableRepository<TAggregateRoot> repository,
            Expression<Func<TAggregateRoot, TProperty>> navigationPropertyPath) where TAggregateRoot : AggregateRoot
        {
            return new IncludableQueryableRepository<TAggregateRoot, TProperty>(repository.Source.Include(navigationPropertyPath));
        }

        public static IIncludableQueryableRepository<TAggregateRoot, TProperty> AlsoInclude<TAggregateRoot, TPreviousProperty, TProperty>(this IIncludableQueryableRepository<TAggregateRoot, TPreviousProperty> repository,
            Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath) where TAggregateRoot : AggregateRoot
        {
            return new IncludableQueryableRepository<TAggregateRoot, TProperty>(repository.ThenInclude(navigationPropertyPath));
        }

        public static IIncludableQueryableRepository<TAggregateRoot, TProperty> AlsoInclude<TAggregateRoot, TPreviousProperty, TProperty>(this IIncludableQueryableRepository<TAggregateRoot, List<TPreviousProperty>> repository,
            Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath) where TAggregateRoot : AggregateRoot
        {
            return new IncludableQueryableRepository<TAggregateRoot, TProperty>(repository.ThenInclude(navigationPropertyPath));
        }
    }
}
