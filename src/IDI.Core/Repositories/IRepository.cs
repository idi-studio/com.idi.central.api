using IDI.Core.Domain;

namespace IDI.Core.Repositories
{
    public interface IRepository<TAggregateRoot>: IQueryRepository<TAggregateRoot> where TAggregateRoot : AggregateRoot
    {
        //IRepositoryContext Context { get; }

        void Add(TAggregateRoot aggregateRoot);

        void Remove(TAggregateRoot aggregateRoot);

        void Update(TAggregateRoot aggregateRoot);

        //bool Exist(Expression<Func<TAggregateRoot, bool>> condition);

        //TAggregateRoot Find(Guid key);

        //TAggregateRoot Find(Guid key, params Expression<Func<TAggregateRoot, dynamic>>[] navigationPropertyPaths);

        //TAggregateRoot Find(Expression<Func<TAggregateRoot, bool>> condition);

        //TAggregateRoot Find(Expression<Func<TAggregateRoot, bool>> condition, params Expression<Func<TAggregateRoot, dynamic>>[] navigationPropertyPaths);

        //List<TAggregateRoot> Get();

        //List<TAggregateRoot> Get(params Expression<Func<TAggregateRoot, dynamic>>[] navigationPropertyPaths);

        //List<TAggregateRoot> Get(Expression<Func<TAggregateRoot, bool>> condition);

        //List<TAggregateRoot> Get(Expression<Func<TAggregateRoot, bool>> condition, params Expression<Func<TAggregateRoot, dynamic>>[] navigationPropertyPaths);

        //QueryableContext<TAggregateRoot> Query();

        //QueryableContext<TAggregateRoot> Query(Expression<Func<TAggregateRoot, bool>> condition);

        //QueryableContext<TAggregateRoot> Query(Expression<Func<TAggregateRoot, bool>> condition, params Expression<Func<TAggregateRoot, dynamic>>[] navigationPropertyPaths);
    }
}
