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

        //TAggregateRoot Find(Guid key, params Expression<Func<TAggregateRoot, dynamic>>[] navigationPropertyPaths);

        TAggregateRoot Find(Expression<Func<TAggregateRoot, bool>> condition);

        //TAggregateRoot Find(Expression<Func<TAggregateRoot, bool>> condition, params Expression<Func<TAggregateRoot, dynamic>>[] navigationPropertyPaths);

        List<TAggregateRoot> Get();

        //List<TAggregateRoot> Get(params Expression<Func<TAggregateRoot, dynamic>>[] navigationPropertyPaths);

        List<TAggregateRoot> Get(Expression<Func<TAggregateRoot, bool>> condition);

        //List<TAggregateRoot> Get(Expression<Func<TAggregateRoot, bool>> condition, params Expression<Func<TAggregateRoot, dynamic>>[] navigationPropertyPaths);

        //QueryableContext<TAggregateRoot> Query();

        //QueryableContext<TAggregateRoot> Query(Expression<Func<TAggregateRoot, bool>> condition);

        //QueryableContext<TAggregateRoot> Query(Expression<Func<TAggregateRoot, bool>> condition, params Expression<Func<TAggregateRoot, dynamic>>[] navigationPropertyPaths);

        //Page<TAggregateRoot> Paging(int number, int size);
    }
}
