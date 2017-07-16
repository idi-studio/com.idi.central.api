using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Central.Domain.Modules.Administration.Commands
{
    public class ClientAuthenticationCommand : Command
    {
        [RequiredField("client-id")]
        public string ClientId { get; private set; }

        [RequiredField("secret-key")]
        public string SecretKey { get; private set; }

        public ClientAuthenticationCommand(string clientId, string secretKey)
        {
            this.ClientId = clientId;
            this.SecretKey = secretKey;
        }
    }
}
