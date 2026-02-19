using UserService.DTOs.UserDTOs;
using UserService.Models;

namespace UserService.Interfaces
{
    public interface IUserRepository
    {
        Task<List<Users>> GetAllUsers();
        Task<Users> GetUser(int userID);
        Task<List<UserTotalSpendingDTO>> GetAllUserTotalSpending();
        Task<UserTotalSpendingDTO> GetUserTotalSpending(int userID);
        Task AddUser(AddUserDTO user);
        Task AddGoogleUser(AddGoogleUserDTO googleUser);
        Task<bool> UpdateUser(int userID, UpdateUserDTO user);
        Task<UserLoginDTO> FindByEmail(string email);
    }
}
