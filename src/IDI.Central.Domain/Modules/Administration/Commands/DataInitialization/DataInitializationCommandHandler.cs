using IDI.Central.Domain.Modules.Administration.AggregateRoots;
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
        public IRepository<Module> ModuleRepository { get; set; }

        [Injection]
        public IRepository<Role> RoleRepository { get; set; }

        [Injection]
        public IRepository<User> UserRepository { get; set; }

        public Result Execute(DataInitializationCommand command)
        {
            bool initialized = this.ModuleRepository.Exist(e => e.Code == command.SeedData.Modules.Administration.Code);

            if (initialized)
                return new Result { Status = ResultStatus.Success, Message = "系统数据已初始化!" };

            this.ModuleRepository.Add(command.SeedData.Modules.Administration);
            this.ModuleRepository.Add(command.SeedData.Modules.OMM);
            this.ModuleRepository.Context.Commit();

            this.UserRepository.Add(command.SeedData.Users.Administrator);
            this.UserRepository.Context.Commit();

            return new Result { Status = ResultStatus.Success, Message = "初始化成功!" };
        }
    }
}
