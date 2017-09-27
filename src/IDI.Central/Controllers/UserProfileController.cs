using IDI.Central.Common;
using IDI.Central.Core;
using IDI.Central.Domain.Modules.Administration.Queries;
using IDI.Central.Models.Administration;
using IDI.Core.Authentication;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/user/profile"), ApplicationAuthorize]
    [Module(Configuration.Modules.BasicInfo)]
    public class UserProfileController : Controller, IAuthorizable
    {
        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;

        public UserProfileController(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
        }

        [HttpGet("{username}")]
        [Permission("user-profile", PermissionType.Read)]
        public Result<MyProfile> Get(string username)
        {
            var condition = new QueryMyProfileCondition { UserName = username };

            return queryProcessor.Execute<QueryMyProfileCondition, MyProfile>(condition);
        }
    }
}
