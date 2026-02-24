using UserService.Models.Users;

namespace UserService.Interfaces
{
    public interface IUserRepository
    {
        Task<List<Users>> GetAllUsers();
        Task<Users> GetUser(int userID);
        Task<List<UserTotalSpendingModel>> GetAllUserTotalSpending();
        Task<UserTotalSpendingModel> GetUserTotalSpending(int userID);
        Task AddUser(AddUserModel user);
        Task AddGoogleUser(AddGoogleUserModel googleUser);
        Task<bool> UpdateUser(int userID, UpdateUserModel user);
        Task<UserLoginModel> FindByEmail(string email);
    }
}
