using System;
using IDI.Central.Common;
using IDI.Central.Core;
using IDI.Central.Domain.Modules.BasicInfo.Commands;
using IDI.Central.Domain.Modules.BasicInfo.Queries;
using IDI.Central.Domain.Modules.Inventory.Queries;
using IDI.Central.Domain.Modules.Sales.Queries;
using IDI.Central.Models.BasicInfo;
using IDI.Central.Models.Inventory;
using IDI.Central.Models.Sales;
using IDI.Core.Authentication;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure.Messaging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace IDI.Central.Controllers
{
    [Route("api/product"), ApplicationAuthorize]
    [Module(Configuration.Modules.BasicInfo)]
    public class ProductController : Controller, IAuthorizable
    {
        private readonly ApplicationOptions options;
        private readonly ICommandBus bus;
        private readonly IQuerier querier;

        public ProductController(IOptionsSnapshot<ApplicationOptions> options, ICommandBus bus, IQuerier querier)
        {
            this.options = options.Value;
            this.bus = bus;
            this.querier = querier;
        }

        [HttpPost]
        [Permission("product", PermissionType.Add)]
        public Result Post([FromBody]ProductInput input)
        {
            var command = new ProductCommand
            {
                Name = input.Name,
                BinCode = input.BinCode,
                Uint = input.Uint,
                SafetyStock = input.SafetyStock,
                SKU = input.SKU,
                StroeId = input.StoreId,
                Tags = input.Tags.ToJson(),
                Enabled = input.Enabled,
                OnShelf = input.OnShelf,
                Mode = CommandMode.Create,
                Group = ValidationGroup.Create,
            };

            return bus.Send(command);
        }

        [HttpGet("list")]
        [Permission("product", PermissionType.Query)]
        public Result<Set<ProductModel>> List()
        {
            return querier.Execute<QueryProductSetCondition, Set<ProductModel>>();
        }

        [HttpGet("selling/{id}")]
        [Permission("product", PermissionType.Query)]
        public Result<Set<SellModel>> Selling(Guid id)
        {
            var condition = new QuerySellSetCondition { CustomerId = id };

            return querier.Execute<QuerySellSetCondition, Set<SellModel>>(condition);
        }

        [HttpPut("{id}")]
        [Permission("product", PermissionType.Modify)]
        public Result Put(Guid id, [FromBody]ProductInput input)
        {
            var command = new ProductCommand
            {
                Id = id,
                Name = input.Name,
                BinCode = input.BinCode,
                Uint = input.Uint,
                SafetyStock = input.SafetyStock,
                SKU = input.SKU,
                StroeId = input.StoreId,
                Tags = input.Tags.ToJson(),
                Enabled = input.Enabled,
                OnShelf = input.OnShelf,
                Mode = CommandMode.Update,
                Group = ValidationGroup.Update,
            };

            return bus.Send(command);
        }

        [HttpGet("{id}")]
        [Permission("product", PermissionType.Read)]
        public Result<ProductModel> Get(Guid id, [FromServices]IHostingEnvironment env)
        {
            var condition = new QueryProductCondition
            {
                Id = id,
                Domain = this.options.Domain,
                SavePath = env.WebRootPath
            };

            return querier.Execute<QueryProductCondition, ProductModel>(condition);
        }

        [HttpDelete("{id}")]
        [Permission("product", PermissionType.Remove)]
        public Result Delete(Guid id)
        {
            var command = new ProductCommand { Id = id, Mode = CommandMode.Delete, Group = ValidationGroup.Delete };

            return bus.Send(command);
        }

        [HttpGet("stocks/{id}")]
        [Permission("product-stocks", PermissionType.Query)]
        public Result<Set<StockModel>> GetStocks(Guid id)
        {
            return querier.Execute<QueryStockSetCondition, Set<StockModel>>(new QueryStockSetCondition { ProductId = id });
        }
    }
}
