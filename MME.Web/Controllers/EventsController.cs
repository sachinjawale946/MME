using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MME.Web.Controllers
{
    // [Authorize]
    public class EventsController : Controller
    {
        [HttpGet, Route("/events-all")]
        public IActionResult Events()
        {
            return View();
        }
    }
}
