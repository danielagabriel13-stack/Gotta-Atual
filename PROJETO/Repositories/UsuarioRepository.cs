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

            var sql = @"INSERT INTO usuarios 
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

            var sql = "SELECT * FROM usuarios WHERE usuario = @UsuarioLogin LIMIT 1";
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
                    Senha = reader.GetString("senha"),
                    etapaConcluida = reader.GetBoolean("etapaConcluida")
                };
            }

            return null!;
        }

        public Usuario BuscarPorId(int usuarioId)
        {
            using var conn = _conexao.Conectar();
            var sql = "SELECT * FROM usuarios WHERE usuario_ID = @UsuarioId LIMIT 1";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);

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
                    Senha = reader.GetString("senha"),
                    etapaConcluida = reader.GetBoolean("etapaConcluida")
                };
            }

            return null!;
        }

        public void AtualizarEtapa(Usuario usuario)
        {
            using var conn = _conexao.Conectar();
            var sql = "UPDATE usuarios SET etapaConcluida = @EtapaConcluida WHERE usuario_ID = @UsuarioId";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@EtapaConcluida", usuario.etapaConcluida);
            cmd.Parameters.AddWithValue("@UsuarioId", usuario.Usuario_ID);
            cmd.ExecuteNonQuery();
        }
    }
}
