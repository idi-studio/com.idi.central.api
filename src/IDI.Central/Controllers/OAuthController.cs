using IDI.Central.Common;
using IDI.Central.Core;
using IDI.Central.Domain.Modules.BasicInfo.Queries;
using IDI.Central.Models.OAuth;
using IDI.Central.Models.OAuth.Inputs;
using IDI.Core.Authentication;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/oauth"), ApplicationAuthorize]
    [Module(Configuration.Modules.OAuth)]
    public class OAuthController : Controller, IAuthorizable
    {
        private readonly ICommandBus bus;
        private readonly IQuerier querier;

        public OAuthController(ICommandBus bus, IQuerier querier)
        {
            this.bus = bus;
            this.querier = querier;
        }

        [HttpPost("token")]
        [Permission("access-token", PermissionType.Read)]
        public Result<AccessTokenModel> Get([FromBody]AccessTokenInput input)
        {
            var condition = new QueryAccessTokenCondition { Code = input.Code, RedirectUri = input.RedirectUri, State = input.State, Type = input.Type };

            return querier.Execute<QueryAccessTokenCondition, AccessTokenModel>(condition);
        }

        [HttpPost("login")]
        [Permission("login", PermissionType.Read)]
        public Result<AccessTokenModel> Get([FromBody]AccessTokenInput input)
        {
            var condition = new QueryAccessTokenCondition { Code = input.Code, RedirectUri = input.RedirectUri, State = input.State, Type = input.Type };

            return querier.Execute<QueryAccessTokenCondition, AccessTokenModel>(condition);
        }
    }
}
