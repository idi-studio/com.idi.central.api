using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.Administration.AggregateRoots;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Verification.Attributes;
using IDI.Core.Localization;
using IDI.Core.Repositories;

namespace IDI.Central.Domain.Modules.Administration.Commands
{
    public class ClientAuthenticationCommand : Command
    {
        [RequiredField]
        public string ClientId { get; private set; }

        [RequiredField]
        public string SecretKey { get; private set; }

        public ClientAuthenticationCommand(string clientId, string secretKey)
        {
            this.ClientId = clientId;
            this.SecretKey = secretKey;
        }
    }

    public class ClientAuthenticationCommandHandler : ICommandHandler<ClientAuthenticationCommand>
    {
        [Injection]
        public ILocalization Localization { get; set; }

        [Injection]
        public IRepository<Client> Clients { get; set; }

        public Result Execute(ClientAuthenticationCommand command)
        {
            var client = this.Clients.Find(e => e.ClientId == command.ClientId);

            if (client == null)
                return Result.Fail(Localization.Get(Resources.Key.Command.InvalidClient));

            if (!client.Active)
                return Result.Fail(Localization.Get(Resources.Key.Command.ClientDisabled));

            string secret = Cryptography.Encrypt(command.SecretKey, client.Salt);

            if (client.SecretKey != secret)
                return Result.Fail(Localization.Get(Resources.Key.Command.ClientAuthenticationFail));

            return Result.Success(message: Localization.Get(Resources.Key.Command.ClientAuthenticationSuccess));
        }
    }
}
