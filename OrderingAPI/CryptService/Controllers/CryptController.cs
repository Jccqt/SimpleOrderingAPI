using CryptService.DTOs;
using CryptService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderingAPI.Shared.Models;

namespace CryptService.Controllers
{
    [Route("api/crypt")]
    [ApiController]
    public class CryptController : ControllerBase
    {
        private readonly AESCryptService _cryptService;

        public CryptController(AESCryptService cryptService)
        {
            _cryptService = cryptService;
        }

        [HttpPost("encrypt")]
        public ActionResult<ServiceResponse<object>> Encrypt(object rawJson)
        {
            var jsonString = System.Text.Json.JsonSerializer.Serialize(rawJson);

            var encryptedString = _cryptService.Encrypt(jsonString);

            return Ok(new ServiceResponse<object>
            {
                Success = true,
                Message = "Encrypted successfully!",
                Data = encryptedString
            });
        }

        [HttpPost("decrypt")]
        public ActionResult<ServiceResponse<object>> Decrypt(PayloadDTO request)
        {
            if (string.IsNullOrEmpty(request.Data))
            {
                return BadRequest(new ServiceResponse<object>
                {
                    Success = false,
                    Message = "Data cannot be empty",
                    Data = null
                });
            }

            var decryptedString = _cryptService.Decrypt(request.Data);

            return Ok(new ServiceResponse<object>
            {
                Success = true,
                Message = "Decrypted successfully!",
                Data = decryptedString
            });
        }

        [HttpPost("decrypt-object")]
        public ActionResult<ServiceResponse<object>> DecryptObject(PayloadDTO request)
        {
            if (string.IsNullOrEmpty(request.Data))
            {
                return BadRequest(new ServiceResponse<object>
                {
                    Success = false,
                    Message = "Data cannot be empty",
                    Data = null
                });
            }

            var decryptedString = _cryptService.DecryptObject<object>(request.Data);

            return Ok(new ServiceResponse<object>
            {
                Success = true,
                Message = "Decrypted successfully!",
                Data = decryptedString
            });
        }
    }
}
