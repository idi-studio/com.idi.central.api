using IDI.Central.Domain.Modules.Common.Queries;
using IDI.Central.Models.Common;
using IDI.Central.Core;
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
        public Result<Set<TagModel>> List()
        {
            return ServiceLocator.QueryProcessor.Execute<QueryTagSetCondition, Set<TagModel>>();
        }
    }
}
