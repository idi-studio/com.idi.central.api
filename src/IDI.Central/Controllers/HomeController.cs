﻿using IDI.Central.Domain.Modules.Administration.Commands;
using IDI.Core.Common;
using IDI.Core.Infrastructure.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICommandBus bus;
        private readonly IQuerier querier;

        public HomeController(ICommandBus bus, IQuerier querier)
        {
            this.bus = bus;
            this.querier = querier;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Swagger()
        {
            var result = bus.Send(new DatabaseInitalCommand());

            if (result.Status == ResultStatus.Success)
                return Redirect("~/swagger/ui/index.html");

            ViewData["Message"] = result.Message;

            return View();
        }

        public IActionResult Central()
        {
            //if (Request.Host.Host.Equals("localhost", StringComparison.CurrentCultureIgnoreCase))
            //    return Redirect("http://localhost:4200/central");

            //return Redirect("http://www.idi-studio.com.cn/central");
            return Redirect("~/central");
        }

        public IActionResult GitHub()
        {
            return Redirect("https://github.com/idi-studio");
        }

        public IActionResult API()
        {
            return Redirect("https://github.com/idi-studio/com.idi.central.api");
        }

        public IActionResult Web()
        {
            return Redirect("https://github.com/idi-studio/com.idi.central.web");
        }
    }
}
