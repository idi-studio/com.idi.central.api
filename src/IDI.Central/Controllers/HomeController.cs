using IDI.Central.Domain.Modules.Administration.Commands;
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

        public IActionResult API()
        {
            var result = bus.Send(new DatabaseInitalCommand());

            if (result.Status == ResultStatus.Success)
                return Redirect("~/swagger/ui/index.html");

            ViewData["Message"] = result.Message;

            return View();
        }

        public IActionResult GitHub() {
            return Redirect("https://github.com/idi-studio");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
    }
}
