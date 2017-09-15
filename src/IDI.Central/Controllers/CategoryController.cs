using System.Collections.Generic;
using IDI.Central.Core;
using IDI.Central.Domain.Modules.Common.Queries;
using IDI.Central.Models.Common;
using IDI.Core.Common;
using IDI.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/catg"), ApplicationAuthorize]
    public class CategoryController : Controller
    {
        [HttpPost]
        public Result<Set<KeyValuePair<int, string>>> Get([FromBody]CategoryInput input)
        {
            var condition = new QueryCategoryCondition { EnumType = input.EnumType };

            return ServiceLocator.QueryProcessor.Execute<QueryCategoryCondition, Set<KeyValuePair<int, string>>>(condition);
        }
    }
}