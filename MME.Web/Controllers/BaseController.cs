using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MME.Web.Filters;
using System.Security.Claims;

namespace MME.Web.Controllers
{
    [Authorize(Policy = "MMECookieScheme")]
    [ServiceFilter(typeof(MMEExceptionFilter))]
    public class BaseController : Controller
    {
        public Guid GetUserId()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Guid.Parse(User.Claims.Where(i => i.Type == ClaimTypes.GroupSid).FirstOrDefault().Value);
            }
            return Guid.Empty;
        }
    }
}
