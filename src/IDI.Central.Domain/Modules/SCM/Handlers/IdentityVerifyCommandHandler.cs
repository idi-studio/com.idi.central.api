using IDI.Central.Domain.Modules.SCM.AggregateRoots;
using IDI.Central.Domain.Modules.SCM.Commands;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.SCM.Handlers
{
    public class IdentityVerifyCommandHandler : ICommandHandler<IdentityVerifyCommand>
    {
        [Injection]
        public IRepository<User> UserRepository { get; set; }

        public Result Execute(IdentityVerifyCommand command)
        {
            var user = this.UserRepository.Find(u => u.UserName == command.UserName);

            if (user == null)
                return new Result { Status = ResultStatus.Fail, Message = "无效的用户名或密码!" };

            string hashed = Cryptography.Encrypt(command.Password, user.Salt);

            if (user.Password != hashed)
                return new Result { Status = ResultStatus.Fail, Message = "无效的用户名或密码!" };

            return new Result { Status = ResultStatus.Success, Message = "认证成功!" };
        }
    }
}
