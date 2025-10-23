using Microsoft.AspNetCore.Mvc;
using GOTTA.Models;
using GOTTA.Repositories;
using GOTTA.ViewModels;

namespace GOTTA.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public IActionResult Participe()
        {
            return View(new CadastroViewModel());
        }

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

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
