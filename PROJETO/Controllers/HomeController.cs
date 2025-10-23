using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GOTTA.Models;
using GOTTA.Repositories;
using GOTTA.ViewModels;



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

        // Página Sobre
        public IActionResult Sobre()
        {
            return View("sobre2"); 
        }

        // Página Mega
        public IActionResult Mega()
        {
            return View();
        }

        // Página Mapa
        public IActionResult Mapa()
        {
            return View();
        }

        // Página Contato
        public IActionResult Contato()
        {
            return View(); 
        }

        // Página Login
        public IActionResult Login()
        {
            return View(); 
        }

        // GET: Participe (formulário de cadastro)
        [HttpGet]
        public IActionResult Participe()
        {
            return View(new CadastroViewModel());
        }

        // POST: Participe (envio do formulário)
        [HttpPost]
        public IActionResult Participe(CadastroViewModel model)
        {
            if (ModelState.IsValid)
            {
                var empresaRepo = new EmpresaRepository();
                var usuarioRepo = new UsuarioRepository();

               
                int empresaId = empresaRepo.Inserir(model.Empresa);
    model.Usuario.Empresa_ID = empresaId;
                usuarioRepo.Inserir(model.Usuario);

                // Redireciona para página inicial ou página de sucesso
                return RedirectToAction("Index");
            }

            // Se houver erro, retorna a mesma View com dados preenchidos
            return View(model);
        }

        // Página Privacy
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
