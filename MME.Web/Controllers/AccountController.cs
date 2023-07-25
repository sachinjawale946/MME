using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MME.Model.Request;
using MME.Web.JWT;
using System.Security.Claims;
using MME.Data;
using MME.Model.Helpers;
using MME.Model.Response;

namespace MME.Web.Controllers
{
    public class AccountController : Controller
    {
        readonly MMEAppDBContext _context;

        public AccountController(MMEAppDBContext context)
        {
            _context = context;
        }

        [HttpGet, Route("/login")]
        public IActionResult Login()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost, Route("/account/login")]
        public IActionResult Login(AuthenticationRequestModel model)
        {
            var user = _context.Users.Where(x => x.Username == model.username).FirstOrDefault();

            if (user == null)
            {
                ModelState.AddModelError("Login", "Member not found.");
                return View();
            }
            else
            {
                var isAuthenticated = PasswordHelper.VerifyPassword(model.password, user.Password, user.PasswordSalt);
                if (isAuthenticated)
                {
                    if (!user.IsActive)
                    {
                        ModelState.AddModelError("Login", "Member profile is deactivated.");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("Login", "Username and Password not matching.");
                    return View();
                }
            }

            var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name,user.Username),
                            new Claim(ClaimTypes.Email,(string.IsNullOrEmpty(user.Email) ? string.Empty: user.Email)),
                            new Claim(ClaimTypes.GroupSid,user.UserId.ToString()),
                            new Claim(ClaimTypes.Role,user.RoleId.ToString()),
                        };
            var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = false,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = false,
                IssuedUtc = DateTime.Now
            };
            var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
            return RedirectToAction("WelcomeCenter", "DashBoard");
        }

        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorized()
        {
            return Unauthorized();
        }
        [HttpGet("forbidden")]
        public IActionResult GetForbidden()
        {
            return Forbid();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(
           CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
