using MySql.Data.MySqlClient;
using System.Data;
using UserService.Interfaces;
using OrderingAPI.Shared.Helpers;
using UserService.DTOs.V1.UserDTOs;
using UserService.Models.Users;
using OrderingAPI.Shared.Models.Responses;
using System.Reflection.Metadata.Ecma335;

namespace UserService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<ServiceResponse<object>> GetAllUsers()
        {
            var response = new ServiceResponse<object>();
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

            if(users.Count > 0)
            {
                response.Success = true;
                response.Message = "Users found.";
                response.Data = users;
            }
            else
            {
                response.Message = "No users found.";
            }

            return response;
        }

        public async Task<ServiceResponse<object>> GetUser(int userID)
        {
            var response = new ServiceResponse<object>();
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("SELECT * FROM users WHERE user_id = @userID", conn);
            cmd.Parameters.AddWithValue("@userID", userID);

            using var reader = await cmd.ExecuteReaderAsync();

            if(await reader.ReadAsync())
            {
                var user =  new Users
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

                response.Success = true;
                response.Message = "User found.";
                response.Data = user;
            }
            else
            {
                response.Message = "No user found.";
            }

            return response;
        }

        public async Task<ServiceResponse<object>> GetAllUserTotalSpending()
        {
            var response = new ServiceResponse<object>();
            List<UserTotalSpendingModel> users = new List<UserTotalSpendingModel>();

            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("SELECT * FROM user_total_spending", conn);

            using var reader = await cmd.ExecuteReaderAsync();

            while(await reader.ReadAsync())
            {
                var user = new UserTotalSpendingModel
                {
                    UserID = Convert.ToInt32(reader["User ID"]),
                    Name = reader["Name"].ToString(),
                    TotalSpending = Convert.ToDecimal(reader["Total Spending"])
                };

                users.Add(user);
            }

            if(users.Count > 0)
            {
                response.Success = true;
                response.Message = "Users total spending found.";
                response.Data = users;
            }
            else
            {
                response.Message = "No user total spending found.";
            }

            return response;
        }

        public async Task<ServiceResponse<object>> GetUserTotalSpending(int userID)
        {
            var response = new ServiceResponse<object>();
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("SELECT * FROM user_total_spending WHERE `User ID` = @userID", conn);
            cmd.Parameters.AddWithValue("@userID", userID);

            using var reader = await cmd.ExecuteReaderAsync();

            if(await reader.ReadAsync())
            {
                var userTotalSpending =  new UserTotalSpendingModel
                {
                    UserID = Convert.ToInt32(reader["User ID"]),
                    Name = reader["Name"].ToString(),
                    TotalSpending = Convert.ToDecimal(reader["Total Spending"])
                };

                response.Success = true;
                response.Message = "User total spending found.";
                response.Data = userTotalSpending;
            }
            else
            {
                response.Message = "No user total spending found.";
            }

            return response;
        }

        public async Task<ServiceResponse> AddUser(AddUserModel user)
        {
            var response = new ServiceResponse();
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            var salt = CustomSecurity.GenerateSalt();

            using var cmd = new MySqlCommand("AddUser", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_full_name", user.FullName);
            cmd.Parameters.AddWithValue("@p_email", user.Email);
            cmd.Parameters.AddWithValue("@p_password", CustomSecurity.HashPassword(user.Password, salt));
            cmd.Parameters.AddWithValue("@p_salt", salt);
            cmd.Parameters.AddWithValue("@p_role", user.Role);

            int rowAffected = await cmd.ExecuteNonQueryAsync();

            if(rowAffected > 0)
            {
                response.Success = true;
                response.Message = "User added successfully.";
            }
            else
            {
                response.Message = "Failed to add user.";
            }

            return response;
        }

        public async Task<ServiceResponse> AddGoogleUser(AddGoogleUserModel googleUser)
        {
            var response = new ServiceResponse();
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            string dummySalt = CustomSecurity.GenerateSalt();
            string dummyPassword = Guid.NewGuid().ToString();
            string hashedDummyPassword = CustomSecurity.HashPassword(dummyPassword, dummySalt);

            using var cmd = new MySqlCommand("AddUser", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_full_name", googleUser.FullName);
            cmd.Parameters.AddWithValue("@p_email", googleUser.Email);
            cmd.Parameters.AddWithValue("@p_password", hashedDummyPassword);
            cmd.Parameters.AddWithValue("@p_salt", dummySalt);
            cmd.Parameters.AddWithValue("@p_role", "Customer");

            int rowAffected = await cmd.ExecuteNonQueryAsync();

            if(rowAffected > 0)
            {
                response.Success = true;
                response.Message = "Google user added successfully.";
            }
            else
            {
                response.Message = "Failed to add Google user.";
            }

            return response;
        }

        public async Task<ServiceResponse> UpdateUser(int userID, UpdateUserModel user)
        {
            var response = new ServiceResponse();
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

            if(rowAffected > 0)
            {
                response.Success = true;
                response.Message = "Successfully updated the user.";
            }
            else
            {
                response.Message = "Failed to update the user.";
            }

            return response;
        }

        public async Task<ServiceResponse<object>> FindByEmail(string email)
        {
            var response = new ServiceResponse<object>();
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("SELECT * FROM users WHERE email = @email", conn);
            cmd.Parameters.AddWithValue("@email", email);

            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                var userLogin =  new UserLoginModel
                {
                    UserID = Convert.ToInt32(reader["user_id"]),
                    FullName = reader["full_name"].ToString(),
                    Email = reader["email"].ToString(),
                    Role = reader["role"].ToString()
                };

                response.Success = true;
                response.Message = "User found.";
                response.Data = userLogin;
            }
            else
            {
                response.Message = "No user found.";
            }

            return response;
        }
    }
}
