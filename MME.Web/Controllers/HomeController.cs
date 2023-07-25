using Microsoft.AspNetCore.Mvc;
using MME.Web.Models;
using System.Diagnostics;

namespace MME.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("/home")]
        public IActionResult Home()
        {
            return View("Index");
        }

		[Route("/about")]
		public IActionResult About()
		{
			return View();
		}

		[Route("/events")]
		public IActionResult Events()
		{
			return View();
		}

		[Route("/members")]
		public IActionResult Members()
		{
			return View();
		}

		[Route("/contact")]
		public IActionResult Contact()
		{
			return View();
		}


		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}