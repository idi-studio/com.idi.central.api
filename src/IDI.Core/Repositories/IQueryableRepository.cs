using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using IDI.Core.Domain;

namespace IDI.Core.Repositories
{
    public interface IQueryableRepository<TAggregateRoot> where TAggregateRoot : AggregateRoot
    {
        IQueryable<TAggregateRoot> Source { get; }

        bool Exist(Expression<Func<TAggregateRoot, bool>> condition);

        int Count(Expression<Func<TAggregateRoot, bool>> condition);

        TAggregateRoot Find(Guid key);

        TAggregateRoot Find(Expression<Func<TAggregateRoot, bool>> condition);

        List<TAggregateRoot> Get();

        List<TAggregateRoot> Get(Expression<Func<TAggregateRoot, bool>> condition);
    }
}
