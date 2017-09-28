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
    [Module(Configuration.Modules.Personal)]
    public class UserProfileController : Controller, IAuthorizable
    {
        private readonly ICommandBus bus;
        private readonly IQuerier querier;

        public UserProfileController(ICommandBus bus, IQuerier querier)
        {
            this.bus = bus;
            this.querier = querier;
        }

        [HttpGet("{username}")]
        [Permission("user-profile", PermissionType.Read)]
        public Result<MyProfile> Get(string username)
        {
            var condition = new QueryMyProfileCondition { UserName = username };

            return querier.Execute<QueryMyProfileCondition, MyProfile>(condition);
        }
    }
}
