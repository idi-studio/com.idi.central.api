using System;
using IDI.Central.Common;
using IDI.Central.Core;
using IDI.Central.Domain.Modules.Inventory.Queries;
using IDI.Central.Models.Inventory;
using IDI.Core.Authentication;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/store"), ApplicationAuthorize]
    [Module(Configuration.Modules.Inventory)]
    public class StoreController : Controller, IAuthorizable
    {
        private readonly ICommandBus bus;
        private readonly IQuerier querier;

        public StoreController(ICommandBus bus, IQuerier querier)
        {
            this.bus = bus;
            this.querier = querier;
        }

        [HttpGet("list")]
        [Permission("list", PermissionType.Query)]
        public Result<Set<StoreModel>> GetStores()
        {
            return querier.Execute<QueryStoreSetCondition, Set<StoreModel>>();
        }

        [HttpGet("{id}")]
        [Permission("store", PermissionType.Read)]
        public Result<StoreModel> GetStocks(Guid id)
        {
            return querier.Execute<QueryStoreCondition, StoreModel>(new QueryStoreCondition { StoreId = id });
        }
    }
}
