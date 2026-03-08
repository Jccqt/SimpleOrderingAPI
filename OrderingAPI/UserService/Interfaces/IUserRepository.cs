using OrderingAPI.Shared.Models.Responses;
using UserService.Models.Users;

namespace UserService.Interfaces
{
    public interface IUserRepository
    {
        Task<ServiceResponse<object>> GetAllUsers();
        Task<ServiceResponse<object>> GetUser(int userID);
        Task<ServiceResponse<object>> GetAllUserTotalSpending();
        Task<ServiceResponse<object>> GetUserTotalSpending(int userID);
        Task<ServiceResponse> AddUser(AddUserModel user);
        Task<ServiceResponse> AddGoogleUser(AddGoogleUserModel googleUser);
        Task<ServiceResponse> UpdateUser(int userID, UpdateUserModel user);
        Task<ServiceResponse<object>> FindByEmail(string email);
    }
}
