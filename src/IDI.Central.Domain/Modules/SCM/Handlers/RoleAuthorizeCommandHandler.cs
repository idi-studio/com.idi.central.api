using System.Linq;
using IDI.Central.Domain.Modules.SCM.AggregateRoots;
using IDI.Central.Domain.Modules.SCM.Commands;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.SCM.Handlers
{
    public class RoleAuthorizeCommandHandler : ICommandHandler<RoleAuthorizeCommand>
    {
        [Injection]
        public IRepository<Role> RoleRepository { get; set; }

        [Injection]
        public IRepository<Privilege> PrivilegeRepository { get; set; }

        public Result Execute(RoleAuthorizeCommand command)
        {
            var role = this.RoleRepository.Find(r => r.Name == command.RoleName, r => r.RolePrivileges);

            if (role == null)
                return new Result { Status = ResultStatus.Fail, Message = "无效的角色!" };

            var recent = command.Privileges.ToList();
            var current = role.RolePrivileges.Select(e => e.PrivilegeId).ToList();
            var deletion = current.Except(recent).ToList();
            var addition = recent.Except(current).ToList();

            role.RolePrivileges.RemoveAll(e => deletion.Contains(e.PrivilegeId));

            var privileges = this.PrivilegeRepository.Get(e => addition.Contains(e.Id));
            var additionPrivileges = privileges.Select(privilege => new RolePrivilege { PrivilegeId = privilege.Id, RoleId = role.Id }).ToList();
            role.RolePrivileges.AddRange(additionPrivileges);

            this.RoleRepository.Update(role);
            this.RoleRepository.Context.Commit();
            this.RoleRepository.Context.Dispose();

            return new Result { Status = ResultStatus.Success, Message = "角色授权成功!" };
        }
    }
}
