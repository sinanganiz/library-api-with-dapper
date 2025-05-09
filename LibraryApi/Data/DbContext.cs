using System.Data;
using Microsoft.Data.Sqlite;

namespace LibraryApi.Data
{
    public class DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection")
                ?? "Data Source=library.db";
        }

        public IDbConnection CreateConnection() => new SqliteConnection(_connectionString);
    }
}