using System.Collections.Generic;
using IDI.Central.Domain.Modules.Retailing.Queries;
using IDI.Central.Providers;
using IDI.Core.Common;
using IDI.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/category"), ApplicationAuthorize]
    public class CategoryController : Controller
    {
        // GET: api/category/{typeName}
        [HttpGet("{typeName}")]
        public Result<Set<KeyValuePair<int, string>>> Get(string typeName)
        {
            var condition = new QueryCategoryCondition { EnumType = typeName };

            return ServiceLocator.QueryProcessor.Execute<QueryCategoryCondition, Set<KeyValuePair<int, string>>>(condition);
        }
    }
}