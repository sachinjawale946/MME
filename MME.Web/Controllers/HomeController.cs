using Microsoft.AspNetCore.Mvc;
using MME.Web.Models;
using System.Diagnostics;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

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
            IReadOnlyList<string> list = new List<string>
            {
                "c0PKHeTXTduKpoaRf3yC1z:APA91bFqpond7qLHo293mx5zQZKe5slRGtfs40Naf1Jnku1dh45jOLmaOtQGZSvfp5ZXNvskPNv-ekU09rOYJKcyvySuNV2wdx0rT3yyZYRFpBUuxNsO5VGvVYa16CJDTksBOjt5D2DD",
            };
            var defaultApp = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "my-community.json")),
            });
            var message = new MulticastMessage()
            {
                Notification = new Notification
                {
                    Title = "Message Title",
                    Body = "Message Body",
                },
                //Topic = "news",
                Tokens = list,
            };
            var messaging = FirebaseMessaging.DefaultInstance;
            var result = messaging.SendMulticastAsync(message).Result;
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