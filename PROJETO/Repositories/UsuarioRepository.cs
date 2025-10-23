using MySql.Data.MySqlClient;
using GOTTA.Database;
using GOTTA.Models;

namespace GOTTA.Repositories
{
    public class UsuarioRepository
    {
        private readonly Conexao _conexao = new Conexao();

        public void Inserir(Usuario usuario)
        {
            using var conn = _conexao.Conectar();

            var sql = @"INSERT INTO Usuarios 
                        (Empresa_ID, Nome, Email, Telefone, Usuario, Senha)
                        VALUES (@empresa_ID, @Nome, @Email, @Telefone, @UsuarioLogin, @Senha)";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@empresa_ID", usuario.Empresa_ID);
            cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
            cmd.Parameters.AddWithValue("@Email", usuario.Email);
            cmd.Parameters.AddWithValue("@Telefone", usuario.Telefone);
            cmd.Parameters.AddWithValue("@UsuarioLogin", usuario.UsuarioLogin);
            cmd.Parameters.AddWithValue("@Senha", usuario.Senha);

            cmd.ExecuteNonQuery();
        }

        public Usuario BuscarPorLogin(string usuarioLogin)
        {
            using var conn = _conexao.Conectar();

            var sql = "SELECT * FROM Usuarios WHERE usuario = @UsuarioLogin LIMIT 1";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UsuarioLogin", usuarioLogin);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Usuario
                {
                    Usuario_ID = reader.GetInt32("usuario_ID"),
                    Empresa_ID = reader.GetInt32("empresa_ID"),
                    Nome = reader.GetString("nome"),
                    Email = reader.GetString("email"),
                    Telefone = reader.GetString("telefone"),
                    UsuarioLogin = reader.GetString("usuario"),
                    Senha = reader.GetString("senha")
                };
            }

            return null;
        }
    }
}
