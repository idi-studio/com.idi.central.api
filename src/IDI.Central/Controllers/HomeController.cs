using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult API()
        {
            ViewData["Message"] = "Your api list page.";

            var initialized = true;

            if (initialized)
                return Redirect("~/swagger/ui/index.html");

            return View();
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
