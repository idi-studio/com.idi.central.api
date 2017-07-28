﻿using IDI.Central.Domain.Modules.Administration.Commands;
using IDI.Central.Domain.Modules.Administration.Queries;
using IDI.Central.Models.Administration;
using IDI.Central.Models.Common;
using IDI.Central.Providers;
using IDI.Core.Common;
using IDI.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/users"), ApplicationAuthorize]
    public class UsersController : Controller
    {
        // POST: api/users
        [HttpPost]
        public Result Post([FromBody]UserRegistrationInput input)
        {
            return ServiceLocator.CommandBus.Send(new UserRegistrationCommand(input.UserName, input.Password, input.Confirm));
        }

        // GET: api/users
        [HttpGet]
        public Result<Table<UserRow>> Get()
        {
            return ServiceLocator.QueryProcessor.Execute<QueryUserCondition, Table<UserRow>>();
        }
    }
}