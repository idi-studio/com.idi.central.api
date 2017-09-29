//using IDI.Central.Common.Enums;
//using IDI.Central.Domain.Localization;
//using IDI.Core.Common;
//using IDI.Core.Infrastructure.Commands;
//using IDI.Core.Infrastructure.DependencyInjection;
//using IDI.Core.Infrastructure.Verification.Attributes;
//using IDI.Core.Localization;
//using IDI.Core.Repositories;

//namespace IDI.Central.Domain.Modules.OAuth.Commands
//{
//    public class AuthorizeCommand : Command
//    {
//        [RequiredField]
//        public string Code { get; set; }

//        public OAuthType Type { get; set; }
//    }

//    public class AuthorizeCommandHandler : ICommandHandler<AuthorizeCommand>
//    {
//        [Injection]
//        public ILocalization Localization { get; set; }

//        //[Injection]
//        //public IRepository<Client> Clients { get; set; }

//        public Result Execute(AuthorizeCommand command)
//        {
//            var client = this.Clients.Find(e => e.ClientId == command.ClientId);

//            if (client == null)
//                return Result.Fail(Localization.Get(Resources.Key.Command.InvalidClient));

//            if (!client.IsActive)
//                return Result.Fail(Localization.Get(Resources.Key.Command.ClientDisabled));

//            string secret = Cryptography.Encrypt(command.SecretKey, client.Salt);

//            if (client.SecretKey != secret)
//                return Result.Fail(Localization.Get(Resources.Key.Command.ClientAuthenticationFail));

//            return Result.Success(message: Localization.Get(Resources.Key.Command.ClientAuthenticationSuccess));
//        }
//    }
//}
