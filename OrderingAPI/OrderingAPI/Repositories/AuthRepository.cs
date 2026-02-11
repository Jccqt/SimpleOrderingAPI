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

        public async Task<bool> Login(LoginRequestDTO login)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("SELECT salt, password FROM users WHERE email = @email && status = 1", conn);
            cmd.Parameters.AddWithValue("@email", login.Email);

            using var reader = await cmd.ExecuteReaderAsync();

            if(await reader.ReadAsync())
            {
                return CustomSecurity.HashPassword(login.Password, reader["salt"].ToString()) == reader["password"].ToString();
            }

            return false;
        }
    }
}
