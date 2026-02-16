using Microsoft.AspNetCore.Identity.Data;
using UserService.DTOs.AuthDTOs;

namespace UserService.Interfaces
{
    public interface IAuthRepository
    {
        Task<LoginResponseDTO> Login(LoginRequestDTO login);
        Task<LoginResponseDTO> RefreshToken(RefreshTokenRequestDTO request);
    }
}
