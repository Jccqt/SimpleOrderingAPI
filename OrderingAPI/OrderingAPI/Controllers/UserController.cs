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

        // GET: api/users?userID={}
        [HttpGet("userID")]
        public async Task<ActionResult<UsersDTO>> GetUser(int userID)
        {
            try
            {
                if(userID == 0)
                {
                    return BadRequest("Invalid User ID.");
                }

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

        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> PostUser(AddUserDTO user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("Invalid input data.");
                }

                Users newUser = new Users
                {
                    full_name = user.FullName,
                    email = user.Email,
                    created_at = DateTime.Now
                };

                await _repository.AddUser(newUser);

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
