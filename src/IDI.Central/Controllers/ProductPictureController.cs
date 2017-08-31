using System;
using System.Linq;
using IDI.Central.Common;
using IDI.Central.Domain.Modules.Retailing.Commands;
using IDI.Central.Models.Retailing;
using IDI.Central.Core;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/product/picture"), ApplicationAuthorize]
    public class ProductPictureController : Controller
    {
        //POST: api/product/picture
        [HttpPost]
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

            return ServiceLocator.CommandBus.Send(command);
        }

        // Put: api/product/picture
        [HttpPut("{id}")]
        public Result Put(Guid id, [FromBody]ProductPictureBatchInput input)
        {
            var command = new ProductPictureBatchCommand
            {
                ProductId = id,
                Pictures = input.Pictures,
                Mode = CommandMode.Update,
                Group = VerificationGroup.Update,
            };

            return ServiceLocator.CommandBus.Send(command);
        }

        // DELETE api/product/picture/{id}
        [HttpDelete("{id}")]
        public Result Delete(Guid id)
        {
            var command = new ProductPictureCommand { Id = id, Mode = CommandMode.Delete, Group = VerificationGroup.Delete };

            return ServiceLocator.CommandBus.Send(command);
        }
    }
}