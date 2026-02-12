using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using OrderingAPI.DTOs.AuthDTOs;
using OrderingAPI.Helpers;
using OrderingAPI.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OrderingAPI.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public AuthRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _configuration = configuration;
        }

        private string CreateToken(string userID, string email, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userID),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO login)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("SELECT user_id, full_name, role, salt, password FROM users WHERE email = @email AND status = 1", conn);
            cmd.Parameters.AddWithValue("@email", login.Email);

            using var reader = await cmd.ExecuteReaderAsync();

            if(await reader.ReadAsync())
            {
                if(CustomSecurity.HashPassword(login.Password, reader["salt"].ToString()) == reader["password"].ToString())
                {
                    string token = CreateToken(
                        reader["user_id"].ToString(),
                        reader["full_name"].ToString(),
                        reader["role"].ToString());

                    return new LoginResponseDTO
                    {
                        Success = true,
                        UserID = reader["user_id"].ToString(),
                        FullName = reader["full_name"].ToString(),
                        Role = reader["role"].ToString(),
                        Token = token
                    };
                }
            }

            return new LoginResponseDTO { Success = false };
        }
    }
}
