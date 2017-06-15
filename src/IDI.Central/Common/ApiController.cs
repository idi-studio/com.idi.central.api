using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IDI.Central.Common
{
    [Authorize(ActiveAuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public abstract class ApiController : Controller
    {

    }
}
