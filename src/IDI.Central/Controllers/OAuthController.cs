using IDI.Central.Common;
using IDI.Central.Core;
using IDI.Central.Domain.Localization;
using IDI.Central.Domain.Modules.BasicInfo.Queries;
using IDI.Central.Models.OAuth;
using IDI.Central.Models.OAuth.Inputs;
using IDI.Core.Authentication;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Messaging;
using IDI.Core.Localization;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/oauth"), ApplicationAuthorize]
    [Module(Configuration.Modules.OAuth)]
    public class OAuthController : Controller, IAuthorizable
    {
        private readonly ICommandBus bus;
        private readonly IQuerier querier;
        private readonly ILocalization localization;

        public OAuthController(ICommandBus bus, IQuerier querier, ILocalization localization)
        {
            this.bus = bus;
            this.querier = querier;
            this.localization = localization;
        }

        //[HttpPost("token")]
        //[Permission("access-token", PermissionType.Read)]
        //public Result<AccessTokenModel> Get([FromBody]AccessTokenInput input)
        //{
        //    var condition = new QueryAccessTokenCondition { Code = input.Code, RedirectUri = input.RedirectUri, State = input.State, Type = input.Type };

        //    return querier.Execute<QueryAccessTokenCondition, AccessTokenModel>(condition);
        //}

        [HttpPost("login")]
        [Permission("login", PermissionType.Read)]
        public Result<CentralTokenModel> Get([FromBody]LoginInput input)
        {
            var tokenResult = querier.Execute<QueryOAuthTokenCondition, OAuthTokenModel>(new QueryOAuthTokenCondition { Code = input.Code, RedirectUri = input.RedirectUri, State = input.State, Type = input.Type });

            if (tokenResult.Status != ResultStatus.Success)
                return Result.Fail<CentralTokenModel>(localization.Get(Resources.Key.Command.AuthFail));

            var userResult = querier.Execute<QueryOAuthUserCondition, OAuthUserModel>(new QueryOAuthUserCondition { AccessToken = tokenResult.Data.AccessToken });

            if (userResult.Status != ResultStatus.Success)
                return Result.Fail<CentralTokenModel>(localization.Get(Resources.Key.Command.RetrieveUserInfoFail));

            var data = new CentralTokenModel();

            return Result.Success(data);
        }
    }
}
