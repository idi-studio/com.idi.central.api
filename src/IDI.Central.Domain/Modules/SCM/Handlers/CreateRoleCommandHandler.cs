using IDI.Central.Domain.Modules.SCM.AggregateRoots;
using IDI.Central.Domain.Modules.SCM.Commands;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.SCM.Handlers
{
    public class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand>
    {
        [Injection]
        public IRepository<Role> RoleRepository { get; set; }

        public Result Execute(CreateRoleCommand command)
        {

            if(this.RoleRepository.Exist(e=>e.Name==command.RoleName))
                return new Result { Status = ResultStatus.Fail, Message = $"角色'{command.RoleName}'已存在!" };

            var role = new Role { Name = command.RoleName };

            this.RoleRepository.Add(role);
            this.RoleRepository.Context.Commit();
            this.RoleRepository.Context.Dispose();

            return new Result { Status = ResultStatus.Success, Message = $"角色'{command.RoleName}'创建成功!" };
        }
    }
}
