using IDI.Core.Domain;

namespace IDI.Core.Repositories
{
    public interface IRepository<TAggregateRoot>: IQueryableRepository<TAggregateRoot> where TAggregateRoot : AggregateRoot
    {
        void Add(TAggregateRoot aggregateRoot);

        void Remove(TAggregateRoot aggregateRoot);

        void Update(TAggregateRoot aggregateRoot);

        int Commit();
    }
}
