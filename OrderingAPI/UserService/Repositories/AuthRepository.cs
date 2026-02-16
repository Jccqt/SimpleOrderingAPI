using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using UserService.DTOs.AuthDTOs;
using OrderingAPI.Shared.Helpers;
using UserService.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Data;

namespace UserService.Repositories
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

        private async Task<UserSessionsDTO> GenerateRefreshToken(int userID)
        {
            var refreshToken = new UserSessionsDTO
            {
                UserID = userID,
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddMinutes(2)
            };

            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("GenerateRefreshToken", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_user_id", refreshToken.UserID);
            cmd.Parameters.AddWithValue("@p_token", refreshToken.Token);
            cmd.Parameters.AddWithValue("@p_expires", refreshToken.Expires);

            await cmd.ExecuteNonQueryAsync();

            return refreshToken;
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                    .GetBytes(_configuration.GetSection("AppSettings:Token").Value)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken || 
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        public async Task<LoginResponseDTO> RefreshToken(RefreshTokenRequestDTO request)
        {
            var principal = GetPrincipalFromExpiredToken(request.ExpiredToken);
            var userID = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = principal.FindFirstValue(ClaimTypes.Role);
            var email = principal.FindFirstValue(ClaimTypes.Email);

            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("SELECT * FROM user_sessions WHERE token = @token", conn);
            cmd.Parameters.AddWithValue("@token", request.RefreshToken);

            using var reader = await cmd.ExecuteReaderAsync();

            if(!await reader.ReadAsync())
            {
                return new LoginResponseDTO
                {
                    Success = false
                };
            }

            var dbSessionID = Convert.ToInt32(reader["id"]);
            var dbUserID = Convert.ToInt32(reader["user_id"]);
            var dbExpires = Convert.ToDateTime(reader["expires"]);

            await reader.CloseAsync();

            if(dbExpires < DateTime.Now)
            {
                await RevokeRefreshTokens(dbUserID);

                return new LoginResponseDTO
                {
                    Success = false
                };
            }

            await RevokeRefreshTokens(dbUserID);

            string newJwtToken = CreateToken(userID, email, userRole);

            var newRefreshToken = await GenerateRefreshToken(int.Parse(userID));

            return new LoginResponseDTO
            {
                Success = true,
                Token = newJwtToken,
                RefreshToken = newRefreshToken.Token,
                UserID = int.Parse(userID),
                Role = userRole
            };

        }

        public async Task RevokeRefreshTokens(int userID)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("DELETE FROM user_sessions WHERE user_id = @userID", conn);
            cmd.Parameters.AddWithValue("@userID", userID);

            await cmd.ExecuteNonQueryAsync();
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
                Expires = DateTime.Now.AddMinutes(1),
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

            using var cmd = new MySqlCommand("SELECT user_id, full_name, email, role, salt, password FROM users WHERE email = @email AND status = 1", conn);
            cmd.Parameters.AddWithValue("@email", login.Email);

            using var reader = await cmd.ExecuteReaderAsync();

            if(await reader.ReadAsync())
            {
                if(CustomSecurity.HashPassword(login.Password, reader["salt"].ToString()) == reader["password"].ToString())
                {
                    string token = CreateToken(
                        reader["user_id"].ToString(),
                        reader["email"].ToString(),
                        reader["role"].ToString());

                    var refreshToken = await GenerateRefreshToken(Convert.ToInt32(reader["user_id"]));

                    return new LoginResponseDTO
                    {
                        Success = true,
                        UserID = Convert.ToInt32(reader["user_id"]),
                        FullName = reader["full_name"].ToString(),
                        Role = reader["role"].ToString(),
                        Token = token,
                        RefreshToken = refreshToken.Token
                    };
                }
            }

            return new LoginResponseDTO { Success = false };
        }
    }
}
