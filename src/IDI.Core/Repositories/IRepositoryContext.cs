using System;
using IDI.Core.Domain;

namespace IDI.Core.Repositories
{
    public interface IRepositoryContext : IUnitOfWork, IDisposable
    {
        Guid Id { get; }

        void RegisterNew<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot;

        void RegisterModified<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot;

        void RegisterDeleted<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot;
    }
}
