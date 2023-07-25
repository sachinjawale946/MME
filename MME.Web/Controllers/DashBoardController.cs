using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MME.Web.Controllers
{
    [Authorize(Policy = "MMECookieScheme")]
    public class DashBoardController : Controller
    {
        public IActionResult WelcomeCenter()
        {
            return View();
        }
    }
}
