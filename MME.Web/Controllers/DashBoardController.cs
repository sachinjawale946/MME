using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MME.Web.Controllers
{
    public class DashBoardController : BaseController
    {
        public IActionResult WelcomeCenter()
        {
            return View();
        }

        [HttpGet, Route("/dashboard")]
        public IActionResult Dashboard()
        {
            return View("WelcomeCenter");
        }
    }
}
