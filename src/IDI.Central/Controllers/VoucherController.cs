using System;
using System.Linq;
using IDI.Central.Common;
using IDI.Central.Common.Enums;
using IDI.Central.Core;
using IDI.Central.Domain.Modules.Sales.Commands;
using IDI.Central.Domain.Modules.Sales.Queries;
using IDI.Central.Models.Sales;
using IDI.Core.Authentication;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/vchr"), ApplicationAuthorize]
    [Module(Configuration.Modules.Sales)]
    public class VoucherController : Controller, IAuthorizable
    {
        private readonly ICommandBus bus;
        private readonly IQuerier querier;

        public VoucherController(ICommandBus bus, IQuerier querier)
        {
            this.bus = bus;
            this.querier = querier;
        }

        [HttpGet("{id}")]
        [Permission("voucher", PermissionType.Read)]
        public Result<VoucherModel> Get(Guid id)
        {
            var condition = new QueryVoucherCondition { Id = id };

            return querier.Execute<QueryVoucherCondition, VoucherModel>(condition);
        }

        [HttpPost]
        [Permission("voucher", PermissionType.Add)]
        public Result Post([FromBody]VoucherInput input)
        {
            var command = new VoucherCommand
            {
                Id = input.Id,
                OrderId = input.OrderId,
                Payment = input.Payment,
                PayMethod = input.PayMethod,
                Remark = input.Remark,
                Mode = CommandMode.Create,
                Group = ValidationGroup.Create,
            };

            return bus.Send(command);
        }

        [HttpPut("{id}")]
        [Permission("voucher", PermissionType.Modify)]
        public Result Put(Guid id, [FromBody]VoucherInput input)
        {
            var command = new VoucherCommand
            {
                Id = id,
                OrderId = input.OrderId,
                Status = input.Status,
                Payment = input.Payment,
                PayMethod = input.PayMethod,
                Remark = input.Remark,
                Mode = CommandMode.Update,
                Group = ValidationGroup.Update,
            };

            return bus.Send(command);
        }

        [HttpPut("paid/{id}")]
        [Permission("voucher", PermissionType.Paid)]
        public Result Paid(Guid id)
        {
            var command = new VoucherCommand
            {
                Id = id,
                Status = TradeStatus.Success,
                Mode = CommandMode.Update,
                Group = ValidationGroup.Update,
            };

            return bus.Send(command);
        }

        [HttpPost("attach")]
        [Permission("voucher", PermissionType.Upload)]
        public Result Attach()
        {
            var command = new VoucherCommand
            {
                Id = this.HttpContext.Request.Form["vchrid"].ToString().ToGuid(),
                File = this.HttpContext.Request.Form.Files.FirstOrDefault(),
                Mode = CommandMode.Upload,
                Group = ValidationGroup.Upload,
            };

            return bus.Send(command);
        }

        [HttpDelete("{id}")]
        [Permission("voucher", PermissionType.Remove)]
        public Result Delete(Guid id)
        {
            var command = new VoucherCommand { Id = id, Mode = CommandMode.Delete, Group = ValidationGroup.Delete };

            return bus.Send(command);
        }
    }
}
