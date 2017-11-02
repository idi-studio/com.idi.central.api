using System;
using IDI.Central.Common;
using IDI.Central.Core;
using IDI.Central.Domain.Modules.Sales.Commands;
using IDI.Central.Domain.Modules.Sales.Queries;
using IDI.Central.Models.Sales;
using IDI.Core.Authentication;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Common.Extensions;
using IDI.Core.Infrastructure.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/prom"), ApplicationAuthorize]
    [Module(Configuration.Modules.Sales)]
    public class PromotionController : Controller, IAuthorizable
    {
        private readonly ICommandBus bus;
        private readonly IQuerier querier;

        public PromotionController(ICommandBus bus, IQuerier querier)
        {
            this.bus = bus;
            this.querier = querier;
        }

        [HttpGet("{id}")]
        [Permission("promotion", PermissionType.Read)]
        public Result<PromotionModel> Get(Guid id)
        {
            var condition = new QueryPromotionCondition { Id = id };

            return querier.Execute<QueryPromotionCondition, PromotionModel>(condition);
        }

        [HttpGet("list")]
        [Permission("promotion", PermissionType.Query)]
        public Result<Set<PromotionModel>> List()
        {
            return querier.Execute<QueryPromotionSetCondition, Set<PromotionModel>>();
        }

        [HttpPost]
        [Permission("promotion", PermissionType.Add)]
        public Result Create([FromBody]PromotionInput input)
        {
            var command = new PromotionCommand
            {
                ProductId = input.ProductId,
                Subject = input.Subject,
                StartTime = input.StartTime.AsDate(),
                EndTime = input.EndTime.AsDate(),
                Enabled = input.Enabled,
                Price = input.Price,
                Mode = CommandMode.Create,
                Group = ValidationGroup.Create,
            };

            return bus.Send(command);
        }

        [HttpPut("{id}")]
        [Permission("promotion", PermissionType.Modify)]
        public Result Update(Guid id, [FromBody]PromotionInput input)
        {
            var command = new PromotionCommand
            {
                Id = id,
                ProductId = input.ProductId,
                Subject = input.Subject,
                StartTime = input.StartTime.AsDate(),
                EndTime = input.EndTime.AsDate(),
                Enabled = input.Enabled,
                Price = input.Price,
                Mode = CommandMode.Update,
                Group = ValidationGroup.Update,
            };

            return bus.Send(command);
        }

        [HttpDelete("{id}")]
        [Permission("promotion", PermissionType.Remove)]
        public Result Delete(Guid id)
        {
            var command = new PromotionCommand { Id = id, Mode = CommandMode.Delete, Group = ValidationGroup.Delete };

            return bus.Send(command);
        }
    }
}
