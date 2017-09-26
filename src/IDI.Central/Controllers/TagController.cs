using IDI.Central.Core;
using IDI.Central.Domain.Modules.Common.Queries;
using IDI.Central.Models.Common;
using IDI.Core.Authentication;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/tag"), ApplicationAuthorize]
    [Module(Common.Constants.Module.Common)]
    public class TagController : Controller, IAuthorizable
    {
        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;

        public TagController(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
        }

        [HttpGet("list")]
        [Permission("tag", PermissionType.Query)]
        public Result<Set<TagModel>> List()
        {
            return queryProcessor.Execute<QueryTagSetCondition, Set<TagModel>>();
        }
    }
}
