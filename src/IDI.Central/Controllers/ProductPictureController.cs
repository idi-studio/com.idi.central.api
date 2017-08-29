﻿using System;
using System.Linq;
using IDI.Central.Domain.Modules.Retailing.Commands;
using IDI.Central.Models.Retailing;
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

        // Put: api/product/picture
        [HttpPut("{id}")]
        public Result Put(Guid id, [FromBody]ProductPictureInput input)
        {
            var command = new ProductPictureCommand
            {
                Id = id,
                Category = input.Category,
                ProductId = input.ProductId,
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