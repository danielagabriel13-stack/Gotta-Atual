using MySql.Data.MySqlClient;
using GOTTA.Database;
using GOTTA.Models;

namespace GOTTA.Repositories
{
    public class EmpresaRepository
    {
        private readonly Conexao _conexao = new Conexao();

        // Inserir empresa e retornar o ID gerado
        public int Inserir(Empresa empresa)
        {
            using var conn = _conexao.Conectar();

            var sql = @"INSERT INTO Empresas (NomeEmpresa, Cnpj, Estado)
                        VALUES (@Nome, @Cnpj, @Estado);
                        SELECT LAST_INSERT_ID();";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Nome", empresa.NomeEmpresa);
            cmd.Parameters.AddWithValue("@Cnpj", empresa.Cnpj);
            cmd.Parameters.AddWithValue("@Estado", empresa.Estado);

            // Executa e retorna o ID inserido
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }

}
