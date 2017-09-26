using System.Collections.Generic;
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
    [Route("api/catg"), ApplicationAuthorize]
    [Module(Common.Constants.Module.Common)]
    public class CategoryController : Controller
    {
        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;

        public CategoryController(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
        }

        [HttpPost]
        [Permission("category", PermissionType.Query)]
        public Result<Set<KeyValuePair<int, string>>> Get([FromBody]CategoryInput input)
        {
            var condition = new QueryCategoryCondition { EnumType = input.EnumType };

            return queryProcessor.Execute<QueryCategoryCondition, Set<KeyValuePair<int, string>>>(condition);
        }
    }
}