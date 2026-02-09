using OrderingAPI.Models;
using MySql.Data.MySqlClient;
using System.Data;
using OrderingAPI.DTOs.UserDTOs;

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

        public async Task<List<UserTotalSpendingDTO>> GetAllUserTotalSpending()
        {
            List<UserTotalSpendingDTO> users = new List<UserTotalSpendingDTO>();

            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("SELECT * FROM user_total_spending", conn);

            using var reader = await cmd.ExecuteReaderAsync();

            while(await reader.ReadAsync())
            {
                var user = new UserTotalSpendingDTO
                {
                    UserID = Convert.ToInt32(reader["User ID"]),
                    Name = reader["Name"].ToString(),
                    TotalSpending = Convert.ToDecimal(reader["Total Spending"])
                };

                users.Add(user);
            }

            return users;
        }

        public async Task<UserTotalSpendingDTO> GetUserTotalSpending(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("SELECT * FROM user_total_spending WHERE `User ID` = @userID", conn);
            cmd.Parameters.AddWithValue("@userID", id);

            using var reader = await cmd.ExecuteReaderAsync();

            if(await reader.ReadAsync())
            {
                return new UserTotalSpendingDTO
                {
                    UserID = Convert.ToInt32(reader["User ID"]),
                    Name = reader["Name"].ToString(),
                    TotalSpending = Convert.ToDecimal(reader["Total Spending"])
                };
            }

            return null;
        }

        public async Task AddUser(AddUserDTO user)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("AddUser", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_full_name", user.FullName);
            cmd.Parameters.AddWithValue("@p_email", user.Email);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<bool> UpdateUser(int id, UpdateUserDTO user)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("UpdateUser", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_user_id", id);
            cmd.Parameters.AddWithValue("@p_full_name", user.FullName);
            cmd.Parameters.AddWithValue("@p_email", user.Email);

            int rowAffected = await cmd.ExecuteNonQueryAsync();

            return rowAffected > 0;
        }
    }
}
