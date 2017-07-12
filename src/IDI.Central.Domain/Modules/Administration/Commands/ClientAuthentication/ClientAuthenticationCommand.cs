using IDI.Core.Infrastructure.Commands;
using IDI.Core.Infrastructure.Verification.Attributes;

namespace IDI.Central.Domain.Modules.Administration.Commands
{
    public class ClientAuthenticationCommand : Command
    {
        [RequiredField("客户端编号")]
        public string ClientId { get; private set; }

        [RequiredField("安全码")]
        public string SecretKey { get; private set; }

        public ClientAuthenticationCommand(string clientId, string secretKey)
        {
            this.ClientId = clientId;
            this.SecretKey = secretKey;
        }
    }
}
