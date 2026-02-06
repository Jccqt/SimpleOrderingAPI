using OrderingAPI.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace OrderingAPI.Repositories
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Users> GetAllUsers()
        {
            List<Users> users = new List<Users>();

            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            using var cmd = new MySqlCommand("SELECT * FROM users", conn);

            using var reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                Users user = new Users
                {
                    user_id = Convert.ToInt32(reader["user_id"]),
                    full_name = reader["full_name"].ToString(),
                    email = reader["email"].ToString(),
                    created_at = Convert.ToDateTime(reader["created_at"])
                };

                users.Add(user);
            }

            return users;
        }
    }
}
