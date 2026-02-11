using Microsoft.AspNetCore.Identity.Data;
using OrderingAPI.DTOs.AuthDTOs;

namespace OrderingAPI.Interfaces
{
    public interface IAuthRepository
    {
        Task<LoginResponseDTO> Login(LoginRequestDTO login);
    }
}
