using Microsoft.AspNetCore.Identity.Data;
using MySql.Data.MySqlClient;
using OrderingAPI.DTOs.AuthDTOs;
using OrderingAPI.Helpers;
using OrderingAPI.Interfaces;

namespace OrderingAPI.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly string _connectionString;

        public AuthRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO login)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("SELECT user_id, full_name, role, salt, password FROM users WHERE email = @email && status = 1", conn);
            cmd.Parameters.AddWithValue("@email", login.Email);

            using var reader = await cmd.ExecuteReaderAsync();

            if(await reader.ReadAsync())
            {
                if(CustomSecurity.HashPassword(login.Password, reader["salt"].ToString()) == reader["password"].ToString())
                {
                    return new LoginResponseDTO
                    {
                        Success = true,
                        UserID = reader["user_id"].ToString(),
                        FullName = reader["full_name"].ToString(),
                        Role = reader["role"].ToString()
                    };
                }
            }

            return new LoginResponseDTO { Success = false };
        }
    }
}
