﻿using System;
using System.Linq;
using IDI.Central.Common;
using IDI.Central.Common.Enums;
using IDI.Central.Core;
using IDI.Central.Domain.Modules.BasicInfo.Commands;
using IDI.Central.Models.BasicInfo;
using IDI.Core.Authentication;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace IDI.Central.Controllers
{
    [Route("api/product/picture"), ApplicationAuthorize]
    [Module(Configuration.Modules.BasicInfo)]
    public class ProductPictureController : Controller, IAuthorizable
    {
        private readonly ICommandBus bus;
        private readonly IQuerier querier;
        private readonly ApplicationOptions options;

        public ProductPictureController(ICommandBus bus, IQuerier querier, IOptionsSnapshot<ApplicationOptions> options)
        {
            this.bus = bus;
            this.querier = querier;
            this.options = options.Value;
        }

        [HttpPost]
        [Permission("product-picture", PermissionType.Upload)]
        //public Result Post([FromServices]IHostingEnvironment env)
        public Result Post()
        {
            var command = new ProductPictureCommand
            {
                //SavePath = env.WebRootPath,
                SavePath = options.UploadFolder,
                ProductId = this.HttpContext.Request.Form["pid"].ToString().ToGuid(),
                Files = this.HttpContext.Request.Form.Files.ToList(),
                Category = ImageCategory.Picture,
                Mode = CommandMode.Create,
                Group = ValidationGroup.Create,
            };

            return bus.Send(command);
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
                Group = ValidationGroup.Update,
            };

            return bus.Send(command);
        }

        [HttpDelete("{id}")]
        [Permission("product-picture", PermissionType.Remove)]
        public Result Delete(Guid id)
        {
            var command = new ProductPictureCommand { Id = id, Mode = CommandMode.Delete, Group = ValidationGroup.Delete };

            return bus.Send(command);
        }
    }
}