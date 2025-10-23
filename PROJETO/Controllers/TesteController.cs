using Microsoft.AspNetCore.Mvc;
using GOTTA.Database;
using MySql.Data.MySqlClient;

namespace GOTTA.Controllers
{
    public class TesteController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                using var conexao = new Conexao().Conectar();
                return Content("Conectou ceretim ;)");
            }
            catch (Exception ex)
            {
                return Content("DEU ERRO. QUE ODIOOOO XP: " + ex.Message);
            }
        }
    }
}
