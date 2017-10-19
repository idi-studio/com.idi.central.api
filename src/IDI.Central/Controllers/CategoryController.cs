using System.Collections.Generic;
using IDI.Central.Common;
using IDI.Central.Core;
using IDI.Central.Domain.Modules.BasicInfo.Queries;
using IDI.Central.Models.BasicInfo;
using IDI.Core.Authentication;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/catg"), ApplicationAuthorize]
    [Module(Configuration.Modules.BasicInfo)]
    public class CategoryController : Controller, IAuthorizable
    {
        private readonly ICommandBus bus;
        private readonly IQuerier querier;

        public CategoryController(ICommandBus bus, IQuerier querier)
        {
            this.bus = bus;
            this.querier = querier;
        }

        [HttpPost("enum")]
        [Permission("enum", PermissionType.Query)]
        public Result<Set<KeyValuePair<int, string>>> Get([FromBody]CategoryInput input)
        {
            var condition = new QueryCategoryCondition { EnumType = input.EnumType };

            return querier.Execute<QueryCategoryCondition, Set<KeyValuePair<int, string>>>(condition);
        }

        [HttpPost("option")]
        [Permission("option", PermissionType.Query)]
        public Result<Set<OptionModel>> GetOptions([FromBody]OptionInput input)
        {
            var condition = new QueryOptionSetCondition { Category = input.Category };

            return querier.Execute<QueryOptionSetCondition, Set<OptionModel>>(condition);
        }
    }
}