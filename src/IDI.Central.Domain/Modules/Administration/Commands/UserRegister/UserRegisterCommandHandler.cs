using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Administration.Commands
{
    public class UserRegisterCommandHandler : ICommandHandler<UserRegisterCommand>
    {
        [Injection]
        public IRepository<User> Users { get; set; }

        public Result Execute(UserRegisterCommand command)
        {
            if (command.Password != command.Confirm)
                return new Result { Status = ResultStatus.Fail, Message = "两次密码不一致" };

            if (this.Users.Exist(u => u.UserName == command.UserName))
                return new Result { Status = ResultStatus.Fail, Message = $"'{command.UserName}'已被注册!" };

            var salt = Cryptography.Salt();
            var user = new User
            {
                UserName = command.UserName,
                Salt = salt,
                Password = Cryptography.Encrypt(command.Password, salt),
                Profile = new UserProfile { Name = command.UserName }
            };

            this.Users.Add(user);
            this.Users.Context.Commit();
            this.Users.Context.Dispose();

            return new Result { Status = ResultStatus.Success, Message = "注册成功!" };
        }
    }
}
