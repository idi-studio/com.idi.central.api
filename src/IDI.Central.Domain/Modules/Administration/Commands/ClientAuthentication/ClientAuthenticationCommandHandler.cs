using IDI.Central.Domain.Localization;
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
                return Result.Fail(Language.Instance.GetByCulture(Resources.Prefix.COMMAND, Resources.Key.INVALID_CLIENT));

            if (!client.IsActive)
                return Result.Fail(Language.Instance.GetByCulture(Resources.Prefix.COMMAND, Resources.Key.CLIENT_DISABLED));

            string secret = Cryptography.Encrypt(command.SecretKey, client.Salt);

            if (client.SecretKey != secret)
                return Result.Fail(Language.Instance.GetByCulture(Resources.Prefix.COMMAND, Resources.Key.CLIENT_AUTHENTICATION_FAIL));

            return Result.Success(message: Language.Instance.GetByCulture(Resources.Prefix.COMMAND, Resources.Key.CLIENT_AUTHENTICATION_SUCCESS));
        }
    }
}
