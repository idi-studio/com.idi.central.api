﻿using System.Collections.Generic;
using IDI.Core.Domain;
using Microsoft.EntityFrameworkCore.Storage;

namespace IDI.Core.Repositories
{
    public interface ITransaction : IDbContextTransaction
    {
        ITransaction Begin();

        bool EnsureCreated();

        void Add<TAggregateRoot>(TAggregateRoot arg) where TAggregateRoot : AggregateRoot;
        void AddRange<TAggregateRoot>(List<TAggregateRoot> args) where TAggregateRoot : AggregateRoot;
        void Update<TAggregateRoot>(TAggregateRoot arg) where TAggregateRoot : AggregateRoot;
        void UpdateRange<TAggregateRoot>(List<TAggregateRoot> args) where TAggregateRoot : AggregateRoot;
        void Remove<TAggregateRoot>(TAggregateRoot arg) where TAggregateRoot : AggregateRoot;

        IQueryableRepository<TAggregateRoot> Source<TAggregateRoot>() where TAggregateRoot : AggregateRoot;
    }
}
