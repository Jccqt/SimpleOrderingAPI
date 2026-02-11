using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using OrderingAPI.DTOs.AuthDTOs;
using OrderingAPI.Interfaces;
using OrderingAPI.Models;

namespace OrderingAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;

        public AuthController(IAuthRepository repository)
        {
            _repository = repository; 
        }

        // POT: api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(LoginRequestDTO login)
        {
            if (login == null)
            {
                return BadRequest(new ServiceResponse<string>
                {
                    Success = false,
                    Message = "User cannot be found"
                });
            }

            bool result = await _repository.Login(login);


            if (!result)
            {
                return Ok(new ServiceResponse<string>
                {
                    Success = false,
                    Message = "Email or Password incorrect."
                });
            }

            return Ok(new ServiceResponse<string>
            {
                Success = true,
                Message = "Login Successful!"
            });
        }
    }
}
