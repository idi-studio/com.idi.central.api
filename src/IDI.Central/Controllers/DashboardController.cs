using System;
using IDI.Central.Common;
using IDI.Central.Core;
using IDI.Central.Domain.Modules.Dashboard.Queries;
using IDI.Central.Domain.Modules.Sales.Commands;
using IDI.Central.Models.Dashboard;
using IDI.Central.Models.Sales.Inputs;
using IDI.Core.Authentication;
using IDI.Core.Common;
using IDI.Core.Common.Enums;
using IDI.Core.Infrastructure.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    [Route("api/dashboard"), ApplicationAuthorize]
    [Module(Configuration.Modules.Dashboard)]
    public class DashboardController : Controller, IAuthorizable
    {
        private readonly IQuerier querier;

        public DashboardController(IQuerier querier)
        {
            this.querier = querier;
        }

        [HttpGet("user-scale")]
        [Permission("user-scale", PermissionType.Report)]
        public Result<UserScaleModel> UserScale()
        {
            return querier.Execute<QueryUserScaleCondition, UserScaleModel>();
        }
    }
}
