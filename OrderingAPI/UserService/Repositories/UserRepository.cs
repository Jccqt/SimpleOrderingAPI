using UserService.Models;
using MySql.Data.MySqlClient;
using System.Data;
using UserService.DTOs.UserDTOs;
using UserService.Interfaces;
using OrderingAPI.Shared.Helpers;

namespace UserService.Repositories
{
    public class UserRepository : IUserRepository
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
                    password = reader["password"].ToString(),
                    salt = reader["salt"].ToString(),
                    role = reader["role"].ToString(),
                    created_at = Convert.ToDateTime(reader["created_at"]),
                    status = Convert.ToInt32(reader["status"])
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
                    password = reader["password"].ToString(),
                    salt = reader["salt"].ToString(),
                    role = reader["role"].ToString(),
                    created_at = Convert.ToDateTime(reader["created_at"]),
                    status = Convert.ToInt32(reader["status"])
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

        public async Task<UserTotalSpendingDTO> GetUserTotalSpending(int userID)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("SELECT * FROM user_total_spending WHERE `User ID` = @userID", conn);
            cmd.Parameters.AddWithValue("@userID", userID);

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

            var salt = CustomSecurity.GenerateSalt();

            using var cmd = new MySqlCommand("AddUser", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_full_name", user.FullName);
            cmd.Parameters.AddWithValue("@p_email", user.Email);
            cmd.Parameters.AddWithValue("@p_password", CustomSecurity.HashPassword(user.Password, salt));
            cmd.Parameters.AddWithValue("@p_salt", salt);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<bool> UpdateUser(int userID, UpdateUserDTO user)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            string salt = null;

            if(user.Password != null)
            {
                salt = CustomSecurity.GenerateSalt();
                user.Password = CustomSecurity.HashPassword(user.Password, salt);
            }

            using var cmd = new MySqlCommand("UpdateUser", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_user_id", userID);
            cmd.Parameters.AddWithValue("@p_full_name", user.FullName);
            cmd.Parameters.AddWithValue("@p_email", user.Email);
            cmd.Parameters.AddWithValue("@p_password", user.Password);
            cmd.Parameters.AddWithValue("@p_salt", salt);
            cmd.Parameters.AddWithValue("@p_role", user.Role);
            cmd.Parameters.AddWithValue("@p_status", user.Status);

            int rowAffected = await cmd.ExecuteNonQueryAsync();

            return rowAffected > 0;
        }
    }
}
