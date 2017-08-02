using IDI.Central.Domain.Localization;
using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Central.Domain.Modules.Administration.Commands
{
    public class ClientAuthenticationCommand : Command
    {
        [RequiredField(Resources.Key.DisplayName.ClientId)]
        public string ClientId { get; private set; }

        [RequiredField(Resources.Key.DisplayName.SecretKey)]
        public string SecretKey { get; private set; }

        public ClientAuthenticationCommand(string clientId, string secretKey)
        {
            this.ClientId = clientId;
            this.SecretKey = secretKey;
        }
    }
}
