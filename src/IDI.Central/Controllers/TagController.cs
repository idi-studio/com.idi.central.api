using IDI.Central.Core;
using IDI.Central.Domain.Modules.Common.Queries;
using IDI.Central.Models.Common;
using IDI.Core.Common;
using IDI.Core.Infrastructure.DependencyInjection;
using IDI.Core.Infrastructure.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/tag"), ApplicationAuthorize]
    public class TagController : Controller
    {
        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;

        public TagController(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
        }

        // GET: api/tag/list
        [HttpGet("list")]
        public Result<Set<TagModel>> List()
        {
            return queryProcessor.Execute<QueryTagSetCondition, Set<TagModel>>();
        }
    }
}
