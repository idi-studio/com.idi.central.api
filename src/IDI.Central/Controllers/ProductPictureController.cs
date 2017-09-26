using System;
using System.Linq;
using IDI.Central.Common.Enums;
using IDI.Central.Core;
using IDI.Central.Domain.Modules.Material.Commands;
using IDI.Central.Models.Material;
using IDI.Core.Authentication;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Messaging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/product/picture"), ApplicationAuthorize]
    [Module(Common.Constants.Module.Common)]
    public class ProductPictureController : Controller, IAuthorizable
    {
        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;

        public ProductPictureController(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
        }

        [HttpPost]
        [Permission("product-picture", PermissionType.Upload)]
        public Result Post([FromServices]IHostingEnvironment env)
        {
            var command = new ProductPictureCommand
            {
                SavePath = env.WebRootPath,
                ProductId = this.HttpContext.Request.Form["pid"].ToString().ToGuid(),
                Files = this.HttpContext.Request.Form.Files.ToList(),
                Category = ImageCategory.Picture,
                Mode = CommandMode.Create,
                Group = VerificationGroup.Create,
            };

            return commandBus.Send(command);
        }

        [HttpPut("{id}")]
        [Permission("product-picture", PermissionType.Modify)]
        public Result Put(Guid id, [FromBody]ProductPictureBatchInput input)
        {
            var command = new ProductPictureBatchCommand
            {
                ProductId = id,
                Pictures = input.Pictures,
                Mode = CommandMode.Update,
                Group = VerificationGroup.Update,
            };

            return commandBus.Send(command);
        }

        [HttpDelete("{id}")]
        [Permission("product-picture", PermissionType.Remove)]
        public Result Delete(Guid id)
        {
            var command = new ProductPictureCommand { Id = id, Mode = CommandMode.Delete, Group = VerificationGroup.Delete };

            return commandBus.Send(command);
        }
    }
}