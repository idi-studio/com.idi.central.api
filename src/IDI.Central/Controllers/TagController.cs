using IDI.Central.Common;
using IDI.Central.Common.JsonTypes;
using IDI.Central.Core;
using IDI.Central.Domain.Modules.BasicInfo.Queries;
using IDI.Core.Authentication;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/tag"), ApplicationAuthorize]
    [Module(Configuration.Modules.BasicInfo)]
    public class TagController : Controller, IAuthorizable
    {
        private readonly ICommandBus bus;
        private readonly IQuerier querier;

        public TagController(ICommandBus bus, IQuerier querier)
        {
            this.bus = bus;
            this.querier = querier;
        }

        [HttpGet("list")]
        [Permission("tag", PermissionType.Query)]
        public Result<Set<Tag>> List()
        {
            return querier.Execute<QueryTagSetCondition, Set<Tag>>();
        }
    }
}
