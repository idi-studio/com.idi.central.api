using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Administration.Commands
{
    public class RoleCreationCommandHandler : ICommandHandler<RoleCreationCommand>
    {
        [Injection]
        public IRepository<Role> Roles { get; set; }

        public Result Execute(RoleCreationCommand command)
        {

            if(this.Roles.Exist(e=>e.Name==command.RoleName))
                return new Result { Status = ResultStatus.Fail, Message = $"角色'{command.RoleName}'已存在!" };

            var role = new Role { Name = command.RoleName };

            this.Roles.Add(role);
            this.Roles.Context.Commit();
            this.Roles.Context.Dispose();

            return new Result { Status = ResultStatus.Success, Message = $"角色'{command.RoleName}'创建成功!" };
        }
    }
}
