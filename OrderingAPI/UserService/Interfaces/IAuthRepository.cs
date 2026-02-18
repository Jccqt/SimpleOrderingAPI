using Microsoft.AspNetCore.Identity.Data;
using UserService.DTOs.AuthDTOs;
using UserService.DTOs.UserDTOs;

namespace UserService.Interfaces
{
    public interface IAuthRepository
    {
        Task<LoginResponseDTO> Login(LoginRequestDTO login);
        Task<UserSessionsDTO> GenerateRefreshToken(int userID);
        Task<LoginResponseDTO> RefreshToken(RefreshTokenRequestDTO request);
        string CreateToken(int userID, string email, string role);
        Task<UserLoginDTO> FindByEmail(string email);
    }
}
