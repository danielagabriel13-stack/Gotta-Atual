using GOTTA.Models;

namespace GOTTA.ViewModels
{
    public class CadastroViewModel
    {
        public Empresa Empresa { get; set; } = new Empresa();
        public Usuario Usuario { get; set; } = new Usuario();
    }
}
