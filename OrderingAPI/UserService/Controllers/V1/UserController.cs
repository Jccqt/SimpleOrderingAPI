using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Interfaces;
using UserService.Repositories;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using Asp.Versioning;
using UserService.DTOs.V1.UserDTOs;
using UserService.Models.Users;
using Microsoft.AspNetCore.RateLimiting;
using OrderingAPI.Shared.Models.Responses;

namespace UserService.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/users")]
    [ApiController]
    [EnableRateLimiting("fixed")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;   
        }

        // GET: api/v1/users
        [HttpGet]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<UsersDTO>>>> GetUsers()
        {
            var result = await _repository.GetAllUsers();

            if (result.Success)
            {
                var users = UserToUsersDTO(result.Data as Users);

                result.Data = users;

                return Ok(users);
            }


            return BadRequest(result);
        }

        // GET: api/v1/users/5
        [HttpGet("{userID}")]
        public async Task<ActionResult<ServiceResponse<UsersDTO>>> GetUser(int userID)
        {
            var result = await _repository.GetUser(userID);

            if (result.Success)
            {
                var user = UserToUsersDTO(result.Data as Users);

                result.Data = user;

                return Ok(user);
            }

            return BadRequest(result);
        }

        // GET: api/v1/users/user-total-spending
        [HttpGet("user-total-spending")]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<UserTotalSpendingDTO>>>> GetUsersTotalSpending()
        {
            var result = await _repository.GetAllUserTotalSpending();

            if (result.Success)
            {
                var usersTotalSpending = UserTotalSpendingMapper(result.Data as UserTotalSpendingModel);

                result.Data = usersTotalSpending;

                return Ok(usersTotalSpending);
            }

            return BadRequest(result);
        }

        // GET: api/v1/users/user-total-spending?userID={}
        [HttpGet("user-total-spending/{userID}")]
        public async Task<ActionResult<ServiceResponse<UserTotalSpendingDTO>>> GetUserTotalSpending(int userID)
        {
            int tokenUserID = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            string tokenRole = User.FindFirstValue(ClaimTypes.Role);

            if(tokenUserID != userID || tokenRole != "Admin")
            {
                return Forbid();
            }

            var result = await _repository.GetUserTotalSpending(userID);

            if (result.Success)
            {
                var userTotalSpending = UserTotalSpendingMapper(result.Data as UserTotalSpendingModel);

                result.Data = userTotalSpending;

                return Ok(userTotalSpending);
            }

            return BadRequest(result);
        }

        // POST: api/v1/users
        [HttpPost]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<AddUserDTO>>> PostUser(AddUserDTO user)
        {
            if (user == null)
            {
                return BadRequest(new ServiceResponse<AddUserDTO>
                {
                    Success = false,
                    Message = "Invalid user data."
                });
            }

            await _repository.AddUser(AddUserMapper(user));

            var response = new ServiceResponse<AddUserDTO>
            {
                Success = true,
                Message = "User added successfully!",
                Data = user
            };

            return Ok(response);
        }

        // PUT: api/v1/users?userID={}
        [HttpPut("{userID}")]
        public async Task<ActionResult<ServiceResponse<UpdateUserDTO>>> UpdateUser(int userID, UpdateUserDTO user)
        {
            int tokenUserID = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            string tokenUserRole = User.FindFirstValue(ClaimTypes.Role);

            if(tokenUserRole != "Admin")
            {
                if(tokenUserID != userID)
                {
                    return Forbid();
                }
            }

            if (user == null)
            {
                return BadRequest(new ServiceResponse<UpdateUserDTO>
                {
                    Success = false,
                    Message = "Invalid user data."
                });
            }

            bool result = await _repository.UpdateUser(userID, UpdateUserMapper(user));

            if (!result)
            {
                return NotFound(new ServiceResponse<UpdateUserDTO>
                {
                    Success = false,
                    Message = "User not found."
                });
            }

            var response = new ServiceResponse<UpdateUserDTO>
            {
                Success = true,
                Message = "User updated successfully!",
                Data = user
            };

            return Ok(response);
        }

        private UsersDTO UserToUsersDTO(Users user) =>
            new UsersDTO
            {
                UserID = user.user_id,
                FullName = user.full_name,
                Email = user.email,
                CreatedAt = user.created_at.ToString("yyyy-MM-dd"),
                Role = user.role,
                Status = user.status == 1 ? "Active" : "Inactive"
            };

        private AddUserModel AddUserMapper(AddUserDTO user) =>
            new AddUserModel
            {
                FullName = user.FullName,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role
            };

        private UpdateUserModel UpdateUserMapper(UpdateUserDTO user) =>
            new UpdateUserModel
            {
                FullName = user.FullName,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role,
                Status = user.Status
            };

        private UserTotalSpendingDTO UserTotalSpendingMapper(UserTotalSpendingModel user) =>
            new UserTotalSpendingDTO
            {
                UserID = user.UserID,
                Name = user.Name,
                TotalSpending = user.TotalSpending
            };
    }
}
