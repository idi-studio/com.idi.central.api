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

        //public Page<TAggregateRoot> Paging(int number, int size)
        //{
        //    if (number <= 0)
        //        throw new ArgumentOutOfRangeException("number", number, "The number should be larger than zero.");

        //    if (size <= 0)
        //        throw new ArgumentOutOfRangeException("size", size, "The size should be larger than zero.");

        //    int skip = (number - 1) * size;
        //    int take = size;
        //    int total = this.queryable.Count();

        //    List<TAggregateRoot> items;

        //    if (sortPredicates.Count > 0)
        //    {
        //        items = this.orderedQueryable.Skip(skip).Take(take).SortBy(sortPredicates).ToList();
        //    }
        //    else
        //    {
        //        items = this.orderedQueryable.Skip(skip).Take(take).ToList();
        //    }

        //    return new Page<TAggregateRoot>(totalRecords: total, totalPages: (total + size - 1) / size, pageSize: size, pageNumber: number, items: items);
        //}

        //public QueryableContext<TAggregateRoot> Query()
        //{
        //    return new QueryableContext<TAggregateRoot>(queryable);
        //}

        //public QueryableContext<TAggregateRoot> Query(Expression<Func<TAggregateRoot, bool>> condition)
        //{
        //    return new QueryableContext<TAggregateRoot>(queryable.Where(condition));
        //}
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
