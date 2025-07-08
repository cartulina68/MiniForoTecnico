using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace MiniForoTecnico.Data
{
    public class Conexion
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public Conexion(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException("Cadena de conexión no encontrada");
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
