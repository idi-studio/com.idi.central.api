using System;
using System.Collections.Generic;
using System.Linq;
using IDI.Core.Domain;

namespace IDI.Core.Repositories
{
    public interface IRepositoryContext : IUnitOfWork, IDisposable
    {
        Guid Id { get; }

        IQueryable<TAggregateRoot> Source<TAggregateRoot>() where TAggregateRoot : AggregateRoot;

        void Add<TAggregateRoot>(TAggregateRoot arg) where TAggregateRoot : AggregateRoot;

        void AddRange<TAggregateRoot>(List<TAggregateRoot> args) where TAggregateRoot : AggregateRoot;

        void Update<TAggregateRoot>(TAggregateRoot arg) where TAggregateRoot : AggregateRoot;

        void Remove<TAggregateRoot>(TAggregateRoot arg) where TAggregateRoot : AggregateRoot;
    }
}
