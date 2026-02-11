using Microsoft.AspNetCore.Identity.Data;
using OrderingAPI.DTOs.AuthDTOs;

namespace OrderingAPI.Interfaces
{
    public interface IAuthRepository
    {
        Task<bool> Login(LoginRequestDTO login);
    }
}
