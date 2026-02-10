using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderingAPI.DTOs.UserDTOs;
using OrderingAPI.Interfaces;
using OrderingAPI.Models;
using OrderingAPI.Repositories;
using System.Data.Common;
using System.Linq;

namespace OrderingAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;   
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<UsersDTO>>>> GetUsers()
        {
            var result = await _repository.GetAllUsers();

            var users = result.Select(u => UserToUsersDTO(u));

            var response = new ServiceResponse<IEnumerable<UsersDTO>>
            {
                Success = true,
                Message = "Users retrieved successfully",
                Data = users
            };

            return Ok(response);
        }

        // GET: api/users/5
        [HttpGet("{userID}")]
        public async Task<ActionResult<ServiceResponse<UsersDTO>>> GetUser(int userID)
        {
            var result = await _repository.GetUser(userID);

            if (result == null)
            {
                return NotFound(new ServiceResponse<UsersDTO>
                {
                    Success = false,
                    Message = "User not found."
                });
            }

            var user = UserToUsersDTO(result);

            var response = new ServiceResponse<UsersDTO>
            {
                Success = true,
                Message = "User retrieved successfully",
                Data = user
            };

            return Ok(response);
        }

        // GET: api/users/user-total-spending
        [HttpGet("user-total-spending")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<UserTotalSpendingDTO>>>> GetUserTotalSpending()
        {
            var users = await _repository.GetAllUserTotalSpending();

            var response = new ServiceResponse<IEnumerable<UserTotalSpendingDTO>>
            {
                Success = true,
                Message = "Users total spending retrieved successfully",
                Data = users
            };

            return Ok(response);
        }

        // GET: api/users/user-total-spending?id={}
        [HttpGet("user-total-spending/{id}")]
        public async Task<ActionResult<ServiceResponse<UserTotalSpendingDTO>>> GetUserTotalSpending(int id)
        {
            var user = await _repository.GetUserTotalSpending(id);

            if (user == null)
            {
                return NotFound(new ServiceResponse<UserTotalSpendingDTO>
                {
                    Success = false,
                    Message = "User total spending not found."
                });
            }

            var response = new ServiceResponse<UserTotalSpendingDTO>
            {
                Success = true,
                Message = "User total spending retrieved successfully",
                Data = user
            };

            return Ok(response);
        }

        // POST: api/users
        [HttpPost]
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

            await _repository.AddUser(user);

            var response = new ServiceResponse<AddUserDTO>
            {
                Success = true,
                Message = "User added successfully!",
                Data = user
            };

            return Ok(response);
        }

        // PUT: api/users?id={}
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<UpdateUserDTO>>> UpdateUser(int id, UpdateUserDTO user)
        {
            if (user == null)
            {
                return BadRequest(new ServiceResponse<UpdateUserDTO>
                {
                    Success = false,
                    Message = "Invalid user data."
                });
            }

            bool result = await _repository.UpdateUser(id, user);

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
                CreatedAt = user.created_at.ToString("yyyy-MM-dd")
            };
    }
}
