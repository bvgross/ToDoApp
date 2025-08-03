using System.Data; //fornece IDbConnection
using Npgsql; //driver pra usar o Postgres com .net

namespace Tarefas.Infrastructure.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection(); //para injeção de dependencia
    }

    public class DbConnectionFactory : IDbConnectionFactory //implementa a interface anterior
    {
        private readonly string _connectionString;

        public DbConnectionFactory(IConfiguration configuration) //Iconfiguration vem do ASP.NET e carrega o appsettings.json
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") //pega o DefaultConnection do appsettings.json
                                ?? throw new InvalidOperationException("Connection string not found.");
        }

        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}