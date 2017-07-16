using System.Collections.Generic;
using IDI.Central.Common;
using IDI.Central.Domain.Modules.Administration.Commands;
using IDI.Central.Models.Administration;
using IDI.Core.Common;
using IDI.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.APIs
{
    [Route("api/user")]
    public class UserController : Controller
    {
        [HttpPost]
        public Result Post([FromBody]UserRegistrationInput input)
        {
            return ServiceLocator.CommandBus.Send(new UserRegistrationCommand(input.UserName, input.Password, input.Confirm));
        }
    }
}
