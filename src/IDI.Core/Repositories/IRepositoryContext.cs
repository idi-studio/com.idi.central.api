using System;
using System.Linq;
using IDI.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace IDI.Core.Repositories
{
    public interface IRepositoryContext : IUnitOfWork, IDisposable
    {
        Guid Id { get; }

        IQueryable<TAggregateRoot> Source<TAggregateRoot>() where TAggregateRoot : AggregateRoot;

        void Add<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot;

        void Update<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot;

        void Remove<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot;
    }
}
