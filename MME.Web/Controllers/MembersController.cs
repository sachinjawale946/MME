using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MME.Web.Controllers
{
    [Authorize(Policy = "MMECookieScheme")]
    public class MembersController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
