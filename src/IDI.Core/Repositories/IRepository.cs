using System.Collections.Generic;
using IDI.Core.Domain;

namespace IDI.Core.Repositories
{
    public interface IRepository<TAggregateRoot>: IQueryableRepository<TAggregateRoot> where TAggregateRoot : AggregateRoot
    {
        void Add(TAggregateRoot arg);

        void AddRange(List<TAggregateRoot> args);

        void Remove(TAggregateRoot arg);

        void Update(TAggregateRoot arg);

        void UpdateRange(List<TAggregateRoot> args);

        int Commit();
    }
}
