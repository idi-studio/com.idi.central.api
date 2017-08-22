using IDI.Central.Domain.Modules.Retailing.Queries;
using IDI.Central.Models.Retailing;
using IDI.Central.Providers;
using IDI.Core.Common;
using IDI.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/tag"), ApplicationAuthorize]
    public class TagController : Controller
    {
        // GET: api/tag/list
        [HttpGet("list")]
        public Result<Set<Tag>> List()
        {
            return ServiceLocator.QueryProcessor.Execute<QueryTagsCondition, Set<Tag>>();
        }
    }
}
