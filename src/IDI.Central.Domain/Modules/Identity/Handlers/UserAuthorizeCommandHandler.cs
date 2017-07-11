using System.Linq;
using IDI.Central.Domain.Modules.Identity.AggregateRoots;
using IDI.Central.Domain.Modules.Identity.Commands;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Identity.Handlers
{
    public class UserAuthorizeCommandHandler : ICommandHandler<UserAuthorizeCommand>
    {
        [Injection]
        public IRepository<User> UserRepository { get; set; }

        [Injection]
        public IRepository<Role> RoleRepository { get; set; }

        public Result Execute(UserAuthorizeCommand command)
        {
            var user = this.UserRepository.Find(e => e.UserName == command.UserName, e => e.UserRoles);

            if (user == null)
                return new Result { Status = ResultStatus.Fail, Message = "无效的用户!" };

            var recent = command.Roles.ToList();
            var current = user.UserRoles.Select(e => e.RoleId).ToList();
            var deletion = current.Except(recent).ToList();
            var addition = recent.Except(current).ToList();

            user.UserRoles.RemoveAll(e => deletion.Contains(e.RoleId));

            var roles = this.RoleRepository.Get(e => addition.Contains(e.Id));
            var additionRoles = roles.Select(role => new UserRole { UserId = user.Id, RoleId = role.Id }).ToList();
            user.UserRoles.AddRange(additionRoles);

            this.UserRepository.Update(user);
            this.UserRepository.Context.Commit();
            this.UserRepository.Context.Dispose();

            return new Result { Status = ResultStatus.Success, Message = "用户角色授权成功!" };
        }
    }
}
