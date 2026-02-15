using OrderingAPI.DTOs.UserDTOs;
using OrderingAPI.Models;

namespace OrderingAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<List<Users>> GetAllUsers();
        Task<Users> GetUser(int userID);
        Task<List<UserTotalSpendingDTO>> GetAllUserTotalSpending();
        Task<UserTotalSpendingDTO> GetUserTotalSpending(int userID);
        Task AddUser(AddUserDTO user);
        Task<bool> UpdateUser(int userID, UpdateUserDTO user);
    }
}
