namespace GOTTA.Models
{
    public class Usuario
    {
        public int Usuario_ID { get; set; }
        public int Empresa_ID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string UsuarioLogin { get; set; }
        public string Senha { get; set; }
    }
}