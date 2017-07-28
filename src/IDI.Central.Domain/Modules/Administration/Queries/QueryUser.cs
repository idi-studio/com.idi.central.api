﻿using System.Linq;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Central.Models.Administration;
using IDI.Central.Models.Common;
using IDI.Core.Common;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Queries;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Administration.Queries
{
    public class QueryUserCondition : Condition { }

    public class QueryUser : Query<QueryUserCondition, Table<UserRow>>
    {
        [Injection]
        public IQueryRepository<User> Users { get; set; }

        public override Result<Table<UserRow>> Execute(QueryUserCondition condition)
        {
            var users = this.Users.Get(u => u.Profile);

            var table = new Table<UserRow>();

            table.Rows = users.OrderBy(r => r.UserName).Select(r => new UserRow
            {
                Id = r.Id,
                UserName = r.UserName,
                IsActive = r.IsActive,
                Name = r.Profile.Name,
                Gender = r.Profile.Gender,
                Birthday = r.Profile.Birthday,
                Photo = r.Profile.Photo
            }).ToList();

            return Result.Success(table);
        }
    }
}