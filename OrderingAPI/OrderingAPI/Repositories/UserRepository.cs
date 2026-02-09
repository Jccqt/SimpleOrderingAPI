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

        public async Task<List<Users>> GetAllUsers()
        {
            List<Users> users = new List<Users>();

            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("SELECT * FROM users", conn);

            using var reader = await cmd.ExecuteReaderAsync();

            while(await reader.ReadAsync())
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

        public async Task<Users> GetUser(int userID)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("SELECT * FROM users WHERE user_id = @userID", conn);
            cmd.Parameters.AddWithValue("@userID", userID);

            using var reader = await cmd.ExecuteReaderAsync();

            if(await reader.ReadAsync())
            {
                return new Users
                {
                    user_id = Convert.ToInt32(reader["user_id"]),
                    full_name = reader["full_name"].ToString(),
                    email = reader["email"].ToString(),
                    created_at = Convert.ToDateTime(reader["created_at"])
                };
            }

            return null;
        }

        public async Task AddUser(Users user)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("AddUser", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_full_name", user.full_name);
            cmd.Parameters.AddWithValue("@p_email", user.email);
            cmd.Parameters.AddWithValue("@p_created_at", user.created_at);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<bool> UpdateUser(Users user)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("UpdateUser", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_user_id", user.user_id);
            cmd.Parameters.AddWithValue("@p_full_name", user.full_name);
            cmd.Parameters.AddWithValue("@p_email", user.email);

            int rowAffected = await cmd.ExecuteNonQueryAsync();

            return rowAffected > 0;
        }
    }
}
