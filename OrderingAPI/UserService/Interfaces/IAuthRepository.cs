using Microsoft.AspNetCore.Identity.Data;
using OrderingAPI.Shared.Models.Responses;
using UserService.DTOs.V1.AuthDTOs;
using UserService.Models.Auth;

namespace UserService.Interfaces
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<object>> Login(string email, string password);
        Task<UserSessionsModel> GenerateRefreshToken(int userID);
        Task<LoginResponseModel> RefreshToken(string expiredToken, string refreshToken);
        string CreateToken(int userID, string email, string role);
    }
}
