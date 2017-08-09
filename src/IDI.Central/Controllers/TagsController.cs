using IDI.Central.Domain.Modules.Retailing.Queries;
using IDI.Central.Models.Retailing;
using IDI.Central.Providers;
using IDI.Core.Common;
using IDI.Core.Common.Basetypes;
using IDI.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/tags"), ApplicationAuthorize]
    public class TagsController : Controller
    {
        // GET: api/tags
        [HttpGet]
        public Result<Collection<Tag>> Get()
        {
            return ServiceLocator.QueryProcessor.Execute<QueryTagsCondition, Collection<Tag>>();
        }
    }
}
