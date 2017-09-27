using System;
using IDI.Central.Common;
using IDI.Central.Core;
using IDI.Central.Domain.Modules.Material.Commands;
using IDI.Central.Domain.Modules.Material.Queries;
using IDI.Central.Domain.Modules.Sales.Queries;
using IDI.Central.Models.Material;
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
        private readonly ICommandBus commandBus;
        private readonly IQuerier queryProcessor;

        public ProductController(IOptionsSnapshot<ApplicationOptions> options, ICommandBus commandBus, IQuerier queryProcessor)
        {
            this.options = options.Value;
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
        }

        [HttpPost]
        [Permission("product", PermissionType.Add)]
        public Result Post([FromBody]ProductInput input)
        {
            var command = new ProductCommand
            {
                Name = input.Name,
                QRCode = input.QRCode,
                Tags = input.Tags.ToJson(),
                Enabled = input.Enabled,
                OnShelf = input.OnShelf,
                Mode = CommandMode.Create,
                Group = VerificationGroup.Create,
            };

            return commandBus.Send(command);
        }

        [HttpGet("list")]
        [Permission("product", PermissionType.Query)]
        public Result<Set<ProductModel>> List()
        {
            return queryProcessor.Execute<QueryProductSetCondition, Set<ProductModel>>();
        }

        [HttpGet("selling/{id}")]
        [Permission("product", PermissionType.Query)]
        public Result<Set<SellModel>> Selling(Guid id)
        {
            var condition = new QuerySellSetCondition { CustomerId = id };

            return queryProcessor.Execute<QuerySellSetCondition, Set<SellModel>>(condition);
        }

        [HttpPut("{id}")]
        [Permission("product", PermissionType.Modify)]
        public Result Put(Guid id, [FromBody]ProductInput input)
        {
            var command = new ProductCommand
            {
                Id = id,
                Name = input.Name,
                QRCode = input.QRCode,
                Tags = input.Tags.ToJson(),
                Enabled = input.Enabled,
                OnShelf = input.OnShelf,
                Mode = CommandMode.Update,
                Group = VerificationGroup.Update,
            };

            return commandBus.Send(command);
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

            return queryProcessor.Execute<QueryProductCondition, ProductModel>(condition);
        }

        [HttpDelete("{id}")]
        [Permission("product", PermissionType.Remove)]
        public Result Delete(Guid id)
        {
            var command = new ProductCommand { Id = id, Mode = CommandMode.Delete, Group = VerificationGroup.Delete };

            return commandBus.Send(command);
        }
    }
}
