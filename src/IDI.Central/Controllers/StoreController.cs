using System;
using System.Linq;
using IDI.Central.Common;
using IDI.Central.Core;
using IDI.Central.Domain.Modules.BasicInfo.Commands;
using IDI.Central.Domain.Modules.Inventory;
using IDI.Central.Domain.Modules.Inventory.Commands;
using IDI.Central.Domain.Modules.Inventory.Queries;
using IDI.Central.Models.Inventory;
using IDI.Central.Models.Inventory.Inputs;
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

        [HttpPost]
        [Permission("store", PermissionType.Add)]
        public Result Post([FromBody]StoreInput input)
        {
            var command = new StoreCommand
            {
                Name = input.Name,
                Active = input.Active,
                Mode = CommandMode.Create,
                Group = VerificationGroup.Create,
            };

            return bus.Send(command);
        }

        [HttpPut("{id}")]
        [Permission("store", PermissionType.Modify)]
        public Result Put(Guid id, [FromBody]StoreInput input)
        {
            var command = new StoreCommand
            {
                Id = id,
                Name = input.Name,
                Active = input.Active,
                Mode = CommandMode.Update,
                Group = VerificationGroup.Update,
            };

            return bus.Send(command);
        }

        [HttpPut("in/{id}")]
        [Permission("in-store", PermissionType.Modify)]
        public Result InStore(Guid id, [FromBody]ChangeStoreInput input)
        {
            var command = new StockInCommand
            {
                StroeId = id,
                Items = input.Items.Select(e => new StockItem { ProductId = e.ProductId, BinCode = e.BinCode, Quantity = e.Quantity }).ToList(),
                Mode = CommandMode.Update,
                Group = VerificationGroup.Update,
            };

            return bus.Send(command);
        }

        [HttpPut("out/{id}")]
        [Permission("out-store", PermissionType.Modify)]
        public Result OutStore(Guid id, [FromBody]ChangeStoreInput input)
        {
            var command = new StockOutCommand
            {
                StroeId = id,
                Items = input.Items.Select(e => new StockItem { ProductId = e.ProductId, BinCode = e.BinCode, Quantity = e.Quantity }).ToList(),
                Mode = CommandMode.Update,
                Group = VerificationGroup.Update,
            };

            return bus.Send(command);
        }

        [HttpDelete("{id}")]
        [Permission("store", PermissionType.Remove)]
        public Result Delete(Guid id)
        {
            var command = new StoreCommand { Id = id, Mode = CommandMode.Delete, Group = VerificationGroup.Delete };

            return bus.Send(command);
        }

        [HttpGet("list")]
        [Permission("store", PermissionType.Query)]
        public Result<Set<StoreModel>> GetStores()
        {
            return querier.Execute<QueryStoreSetCondition, Set<StoreModel>>();
        }

        [HttpGet("stock-options")]
        [Permission("stock-options", PermissionType.Query)]
        public Result<Set<StockOptionModel>> GetStockOptions()
        {
            return querier.Execute<QueryStockOptionSetCondition, Set<StockOptionModel>>();
        }

        [HttpGet("{id}")]
        [Permission("store", PermissionType.Read)]
        public Result<StoreModel> GetStocks(Guid id)
        {
            return querier.Execute<QueryStoreCondition, StoreModel>(new QueryStoreCondition { StoreId = id });
        }
    }
}
