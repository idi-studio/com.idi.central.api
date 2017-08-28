using System;
using System.Linq;
using IDI.Central.Domain.Modules.Retailing.Commands;
using IDI.Central.Providers;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/product/picture"), ApplicationAuthorize]
    public class ProductPictureController : Controller
    {
        //POST: api/product/picture
        [HttpPost]
        public Result Post()
        {
            var command = new ProductPictureCommand
            {
                ProductId = this.HttpContext.Request.Form["pid"].ToString().ToGuid(),
                Files = this.HttpContext.Request.Form.Files.ToList(),
                Mode = CommandMode.Create,
                Group = VerificationGroup.Create,
            };

            return ServiceLocator.CommandBus.Send(command);
        }
    }
}