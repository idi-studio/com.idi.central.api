using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using IDI.Core.Domain;
using Microsoft.EntityFrameworkCore.Storage;

namespace IDI.Core.Repositories
{
    public interface ITransaction : IDbContextTransaction
    {
        ITransaction Begin();

        void Add<TAggregateRoot>(TAggregateRoot arg) where TAggregateRoot : AggregateRoot;
        void AddRange<TAggregateRoot>(List<TAggregateRoot> args) where TAggregateRoot : AggregateRoot;
        void Update<TAggregateRoot>(TAggregateRoot arg) where TAggregateRoot : AggregateRoot;
        void Remove<TAggregateRoot>(TAggregateRoot arg) where TAggregateRoot : AggregateRoot;

        IQueryableRepository<TAggregateRoot> Source<TAggregateRoot>() where TAggregateRoot : AggregateRoot;
    }
}
