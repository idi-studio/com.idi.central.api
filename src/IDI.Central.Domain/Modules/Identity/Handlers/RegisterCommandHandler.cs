using IDI.Central.Domain.Modules.Identity.AggregateRoots;
using IDI.Central.Domain.Modules.Identity.Commands;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Identity.Handlers
{
    public class RegisterCommandHandler : ICommandHandler<RegisterCommand>
    {
        [Injection]
        public IRepository<User> UserRepository { get; set; }

        public Result Execute(RegisterCommand command)
        {
            if (command.Password != command.Confirm)
                return new Result { Status = ResultStatus.Fail, Message = "两次密码不一致" };

            if (this.UserRepository.Exist(u => u.UserName == command.UserName))
                return new Result { Status = ResultStatus.Fail, Message = $"'{command.UserName}'已被注册!" };

            var salt = Cryptography.Salt();
            var user = new User
            {
                UserName = command.UserName,
                Salt = salt,
                Password = Cryptography.Encrypt(command.Password, salt),
                Profile = new UserProfile { Name = command.UserName }
            };

            this.UserRepository.Add(user);
            this.UserRepository.Context.Commit();
            this.UserRepository.Context.Dispose();

            return new Result { Status = ResultStatus.Success, Message = "注册成功!" };
        }
    }
}
