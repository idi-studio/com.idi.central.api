using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Localization;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Administration.Commands
{
    public class ClientAuthenticationCommandHandler : ICommandHandler<ClientAuthenticationCommand>
    {
        [Injection]
        public IRepository<Client> Clients { get; set; }

        public Result Execute(ClientAuthenticationCommand command)
        {
            var client = this.Clients.Find(e => e.ClientId == command.ClientId);

            if (client == null)
                return Result.Fail(Language.Instance.GetByCulture("command", "invalid-client"));

            if (!client.IsActive)
                return Result.Fail(Language.Instance.GetByCulture("command", "invalid-disabled"));

            string secret = Cryptography.Encrypt(command.SecretKey, client.Salt);

            if (client.SecretKey != secret)
                return Result.Fail(Language.Instance.GetByCulture("command", "client-authentication-fail"));

            return Result.Success(message: Language.Instance.GetByCulture("command", "client-authentication-success"));
        }
    }
}
