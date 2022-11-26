using Microsoft.Data.SqlClient;

namespace Tour_Ready_Capstone.Repositories
{
    public class BaseRepository
    {
        private readonly string _connectionString;
        protected BaseRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public SqlConnection Connection => new SqlConnection(_connectionString);
    }
}
