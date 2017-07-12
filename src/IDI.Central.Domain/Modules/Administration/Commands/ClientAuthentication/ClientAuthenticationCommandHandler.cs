using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Administration.Commands
{
    public class ClientAuthenticationCommandHandler : ICommandHandler<ClientAuthenticationCommand>
    {
        [Injection]
        public IRepository<Client> Clients { get; set; }

        public Result Execute(ClientAuthenticationCommand command)
        {
            var client = this.Clients.Find(e => e.ClientId == command.ClientId && e.SecretKey == command.SecretKey);

            if (client == null)
                return Result.Fail("无效的客户端!");

            if (!client.IsActive)
                return Result.Fail("该客户端已被禁用!");

            return Result.Success(message: "认证成功!");
        }
    }
}
