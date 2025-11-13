using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GOTTA.Models;
using GOTTA.Repositories;
using System.Security.Claims;

namespace GOTTA.Controllers
{
    [Authorize]
    public class VerifyStepController : Controller
    {
        private readonly UsuarioRepository _usuarioRepo = new UsuarioRepository();

   
        public IActionResult Index()
        {
            int usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var usuario = _usuarioRepo.BuscarPorId(usuarioId);

            if (usuario == null)
                return RedirectToAction("Login", "Home");

            
            if (usuario.etapaConcluida)
                return RedirectToAction("Index", "Home");

            return View(usuario); 
        }

        
        [HttpPost]
        public IActionResult Concluir()
        {
            int usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var usuario = _usuarioRepo.BuscarPorId(usuarioId);

            if (usuario == null)
                return RedirectToAction("Login", "Home");

            usuario.etapaConcluida = true;
            _usuarioRepo.AtualizarEtapa(usuario);

           
            return RedirectToAction("Index", "Home");
        }
    }
}
