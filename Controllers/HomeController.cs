using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GOTTA.Models;

namespace GOTTA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Página inicial
        public IActionResult Index()
        {
            return View("Home"); // Views/Home/Home.cshtml
        }

        // Outras páginas
        public IActionResult Sobre()
        {
            return View("sobre2"); 
        }

        public IActionResult Mega()
        {
            return View();
        }

        public IActionResult Mapa()
        {
            return View();
        }

        public IActionResult Contato()
        {
            return View(); 
        }

        public IActionResult Login()
        {
            return View(); 
        }

        public IActionResult Participe()
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
