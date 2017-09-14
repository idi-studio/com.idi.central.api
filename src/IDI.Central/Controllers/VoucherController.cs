using System;
using System.Linq;
using IDI.Central.Common.Enums;
using IDI.Central.Core;
using IDI.Central.Domain.Modules.Retailing.Commands;
using IDI.Central.Domain.Modules.Retailing.Queries;
using IDI.Central.Models.Retailing;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/vchr"), ApplicationAuthorize]
    public class VoucherController : Controller
    {
        [HttpGet("{id}")]
        public Result<VoucherModel> Get(Guid id)
        {
            var condition = new QueryVoucherCondition { Id = id };

            return ServiceLocator.QueryProcessor.Execute<QueryVoucherCondition, VoucherModel>(condition);
        }

        [HttpPost]
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
                Group = VerificationGroup.Create,
            };

            return ServiceLocator.CommandBus.Send(command);
        }

        [HttpPut("{id}")]
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
                Group = VerificationGroup.Update,
            };

            return ServiceLocator.CommandBus.Send(command);
        }

        [HttpPut("paid/{id}")]
        public Result Paid(Guid id)
        {
            var command = new VoucherCommand
            {
                Id = id,
                Status = TradeStatus.Success,
                Mode = CommandMode.Update,
                Group = VerificationGroup.Update,
            };

            return ServiceLocator.CommandBus.Send(command);
        }

        [HttpPost("attach")]
        public Result Attach()
        {
            var command = new VoucherCommand
            {
                Id = this.HttpContext.Request.Form["vchrid"].ToString().ToGuid(),
                File = this.HttpContext.Request.Form.Files.FirstOrDefault(),
                Mode = CommandMode.Upload,
                Group = VerificationGroup.Upload,
            };

            return ServiceLocator.CommandBus.Send(command);
        }

        [HttpDelete("{id}")]
        public Result Delete(Guid id)
        {
            var command = new VoucherCommand { Id = id, Mode = CommandMode.Delete, Group = VerificationGroup.Delete };

            return ServiceLocator.CommandBus.Send(command);
        }
    }
}
