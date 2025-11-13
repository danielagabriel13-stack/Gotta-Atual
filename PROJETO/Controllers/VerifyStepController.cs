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

        // Página que mostra a verificação
        public IActionResult Index()
        {
            int usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var usuario = _usuarioRepo.BuscarPorId(usuarioId);

            if (usuario == null)
                return RedirectToAction("Login", "Home");

            // Se já concluiu a etapa, redireciona direto
            if (usuario.etapaConcluida)
                return RedirectToAction("Index", "Home");

            return View(usuario); // envia para view
        }

        // Método que atualiza a etapa concluída
        [HttpPost]
        public IActionResult Concluir()
        {
            int usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var usuario = _usuarioRepo.BuscarPorId(usuarioId);

            if (usuario == null)
                return RedirectToAction("Login", "Home");

            usuario.etapaConcluida = true;
            _usuarioRepo.AtualizarEtapa(usuario);

            // Redireciona para a Home ou qualquer página desejada
            return RedirectToAction("Index", "Home");
        }
    }
}
