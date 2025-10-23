using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GOTTA.Database
{
    public class Conexao
    {
        private readonly string _connectionString;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Conexao()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

#pragma warning disable CS8601 // Possible null reference assignment.
            _connectionString = builder.GetConnectionString("DefaultConnection");
#pragma warning restore CS8601 // Possible null reference assignment.
        }

        public MySqlConnection Conectar()
        {
            var conexao = new MySqlConnection(_connectionString);
            conexao.Open();
            return conexao;
        }
    }
}
