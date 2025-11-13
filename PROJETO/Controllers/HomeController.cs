using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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

        // ✅ Páginas públicas
        [AllowAnonymous]
        public IActionResult Index() => View("Home");

        [AllowAnonymous]
        public IActionResult Sobre() => View("sobre2");

        [AllowAnonymous]
        public IActionResult Login() => View();

        // ✅ LOGIN COM VERIFICAÇÃO DE ETAPA
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string usuarioLogin, string senha)
        {
            var usuarioRepo = new UsuarioRepository();
            var usuario = usuarioRepo.BuscarPorLogin(usuarioLogin);

            if (usuario == null || usuario.Senha != senha)
            {
                ViewBag.Erro = "Usuário ou senha incorretos.";
                return View();
            }

            // 🔐 Cria os claims do usuário
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.NameIdentifier, usuario.Usuario_ID.ToString()),
                new Claim("Usuario", usuario.UsuarioLogin)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(
                "CookieAuth",
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );

            // 🚧 Verifica se a etapa foi concluída
            if (!usuario.etapaConcluida)
            {
                // Redireciona o usuário para a verificação
                return RedirectToAction("Index", "VerifyStep", new { usuarioId = usuario.Usuario_ID });
            }

            // ✅ Se já concluiu, vai para a Home normalmente
            return RedirectToAction("Index");
        }

        // ✅ Logout
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index", "Home");
        }

        // ✅ Cadastro (Participe)
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Participe()
        {
            return View(new CadastroViewModel());
        }

        [AllowAnonymous]
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

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // ✅ Páginas protegidas (apenas logado)
        [Authorize]
        public IActionResult Mega() => View();

        [Authorize]
        public IActionResult Mapa() => View();

        [Authorize]
        public IActionResult Contato() => View();

        [AllowAnonymous]
        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
