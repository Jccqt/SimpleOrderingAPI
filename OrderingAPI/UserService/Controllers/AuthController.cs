using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using UserService.DTOs.AuthDTOs;
using UserService.Interfaces;
using UserService.Models;
using OrderingAPI.Shared.Models;
using Google.Apis.Auth;
using UserService.DTOs.UserDTOs;

namespace UserService.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepository;
        private readonly IUserRepository userRepository;

        public AuthController(IAuthRepository repository, IUserRepository userRepository)
        {
            authRepository = repository;
            this.userRepository = userRepository;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<LoginResponseDTO>>> Login(LoginRequestDTO login)
        {
            if (login == null)
            {
                return BadRequest(new ServiceResponse<LoginResponseDTO>
                {
                    Success = false,
                    Message = "User cannot be found"
                });
            }

            var result = await authRepository.Login(login);


            if (!result.Success)
            {
                return Ok(new ServiceResponse<LoginResponseDTO>
                {
                    Success = false,
                    Message = "Email or Password incorrect."
                });
            }

            return Ok(new ServiceResponse<LoginResponseDTO>
            {
                Success = true,
                Message = "Login Successful!",
                Data = result
            });
        }

        // POST: api/auth/google-login
        [HttpPost("google-login")]
        public async Task<ActionResult<ServiceResponse<LoginResponseDTO>>> GoogleLogin(GoogleLoginDTO request)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { "551755919815-u03hnh1p0hj7pn74ledieuib142p1obi.apps.googleusercontent.com" }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);

            var email = payload.Email;
            var name = payload.Name;

            var user = await userRepository.FindByEmail(email);

            if(user == null)
            {
                var googleUser = new AddGoogleUserDTO
                {
                    Email = email,
                    FullName = name
                };

                await userRepository.AddGoogleUser(googleUser);

                user = await userRepository.FindByEmail(email);
            }

            var token = authRepository.CreateToken(user.UserID, user.Email, user.Role);

            var refreshToken = await authRepository.GenerateRefreshToken(Convert.ToInt32(user.UserID));

            return Ok(new ServiceResponse<LoginResponseDTO>
            {
                Success = true,
                Message = "Google Login Successful",
                Data = new LoginResponseDTO
                {
                    Success = true,
                    UserID = Convert.ToInt32(user.UserID),
                    FullName = user.FullName,
                    Role = user.Role,
                    Token = token,
                    RefreshToken = refreshToken.Token
                }
            });
        }

        // POST: api/auth/refresh-token
        [HttpPost("refresh-token")]
        public async Task<ActionResult<ServiceResponse<LoginResponseDTO>>> RefreshToken(RefreshTokenRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest(new ServiceResponse<LoginResponseDTO>{
                    Success = false,
                    Message = "Invalid Refresh Token Request Data"
                });
            }

            var response = await authRepository.RefreshToken(request);

            if (!response.Success)
            {
                return Unauthorized(response);
            }

            return Ok(response);
        }
    }
}
