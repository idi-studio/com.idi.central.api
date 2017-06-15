using IDI.Central.Domain.Modules.SCM.AggregateRoots;
using IDI.Central.Domain.Modules.SCM.Commands;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.SCM.Handlers
{
    public class InitializeCommandHandler : ICommandHandler<InitializeCommand>
    {
        [Injection]
        public IRepository<Module> ModuleRepository { get; set; }

        [Injection]
        public IRepository<Role> RoleRepository { get; set; }

        [Injection]
        public IRepository<User> UserRepository { get; set; }

        public Result Execute(InitializeCommand command)
        {
            bool initialized = this.ModuleRepository.Exist(e => e.Code == command.SeedData.Modules.SCM.Code);

            if (initialized)
                return new Result { Status = ResultStatus.Success, Message = "系统数据已初始化!" };

            this.ModuleRepository.Add(command.SeedData.Modules.SCM);
            this.ModuleRepository.Add(command.SeedData.Modules.OMM);
            this.ModuleRepository.Context.Commit();

            this.UserRepository.Add(command.SeedData.Users.Administrator);
            this.UserRepository.Context.Commit();

            return new Result { Status = ResultStatus.Success, Message = "初始化成功!" };
        }
    }
}
