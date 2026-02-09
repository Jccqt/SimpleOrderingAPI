using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderingAPI.DTOs.UserDTOs;
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
        private readonly UserRepository _repository;

        public UserController(UserRepository repository)
        {
            _repository = repository;   
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersDTO>>> GetUsers()
        {
            try
            {
                var result = await _repository.GetAllUsers();

                var users = result.Select(u => UserToUsersDTO(u));

                return Ok(users);
            }
            catch (DbException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured on database.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured.");
            }
        }

        // GET: api/users/5
        [HttpGet("{userID}")]
        public async Task<ActionResult<UsersDTO>> GetUser(int userID)
        {
            try
            {
                var result = await _repository.GetUser(userID);

                if(result == null)
                {
                    return NotFound("User not found.");
                }

                var user = UserToUsersDTO(result);

                return Ok(user);
            }
            catch (DbException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured on database.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured.");
            }
        }

        // GET: api/users/user-total-spending
        [HttpGet("user-total-spending")]
        public async Task<ActionResult<IEnumerable<UserTotalSpendingDTO>>> GetUserTotalSpending()
        {
            try
            {
                var users = await _repository.GetAllUserTotalSpending();

                return Ok(users);
            }
            catch (DbException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured on database.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured.");
            }
        }

        // GET: api/users/user-total-spending?id={}
        [HttpGet("user-total-spending/{id}")]
        public async Task<ActionResult<UserTotalSpendingDTO>> GetUserTotalSpending(int id)
        {
            try
            {
                var user = await _repository.GetUserTotalSpending(id);

                if (user == null)
                {
                    return NotFound("User total spending not found.");
                }

                return Ok(user);
            }
            catch (DbException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured on database.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured.");
            }
        }

        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> PostUser(AddUserDTO user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("Invalid user data.");
                }

                await _repository.AddUser(user);

                return Ok("User added successfully!");
            }
            catch (DbException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured on database.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured.");
            }
        }

        // PUT: api/users?id={}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserDTO user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("Invalid user data.");
                }

                bool result = await _repository.UpdateUser(id, user);

                if (!result)
                {
                    return NotFound("User not found.");
                }

                return NoContent();
            }
            catch (DbException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured on database.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured.");
            }
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
