﻿using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Central.Domain.Modules.Administration.Commands;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Administration.Commands
{
    public class DataInitializationCommandHandler : ICommandHandler<DataInitializationCommand>
    {
        [Injection]
        public IRepository<Module> Modules { get; set; }

        [Injection]
        public IRepository<Role> Roles { get; set; }

        [Injection]
        public IRepository<User> Users { get; set; }

        public Result Execute(DataInitializationCommand command)
        {
            bool initialized = this.Modules.Exist(e => e.Code == command.SeedData.Modules.Administration.Code);

            if (initialized)
                return new Result { Status = ResultStatus.Success, Message = "系统数据已初始化!" };

            this.Modules.Add(command.SeedData.Modules.Administration);
            this.Modules.Add(command.SeedData.Modules.OMM);
            this.Modules.Context.Commit();

            this.Users.Add(command.SeedData.Users.Administrator);
            this.Users.Context.Commit();

            return new Result { Status = ResultStatus.Success, Message = "初始化成功!" };
        }
    }
}
