using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;

namespace GOTTA.Controllers
{
    [AllowAnonymous]
    public class NavBarController : Controller
    {
        // Retorna a navbar correta conforme login
        public IActionResult Navbar()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                // Usuário logado → Navbar com Mega, Mapa, Contato e botão de logout
                return PartialView("_NavAutenticado");
            }
            else
            {
                // Usuário anônimo → Navbar com Home, Sobre, Participe e Login
                return PartialView("_NavAnonimo");
            }
        }

        // GET: Login
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                // Redireciona usuário logado para Home
                return RedirectToAction("Index", "Home");
            }

            return View("~/Views/Home/Login.cshtml");
        }

        // POST: Logout
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index", "Home");
        }
    }
}
