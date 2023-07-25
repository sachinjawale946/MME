using Microsoft.AspNetCore.Mvc;
using MME.Model.Request;

namespace MME.Web.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet, Route("/login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, Route("/account/login")]
        public IActionResult Login(AuthenticationRequestModel model)
        {
            return View();
        }
    }
}
