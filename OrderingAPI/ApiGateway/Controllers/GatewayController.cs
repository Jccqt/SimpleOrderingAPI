using ApiGateway.DTOs;
using ApiGateway.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderingAPI.Shared.Models;
using OrderingAPI.Shared.Security;
using Org.BouncyCastle.Asn1.Ocsp;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/{*path}")]
    public class GatewayController : ControllerBase
    {
        private readonly IGatewayRepository _repository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AESCryptService _crypService;

        public GatewayController(IGatewayRepository repository, IHttpClientFactory httpClientFactory, AESCryptService crypService)
        {
            _repository = repository;
            _httpClientFactory = httpClientFactory;
            _crypService = crypService;
        }

        [AcceptVerbs("GET", "POST", "PUT")]
        public async Task<IActionResult> HandleRequests(string path)
        {
            var serviceKey = path.Split('/')[0];
            var route = await _repository.GetRouteByPath(serviceKey);

            if (route == null)
            {
                return NotFound(new ServiceResponse<object>
                {
                    Success = false,
                    Message = $"Service {serviceKey} not found."
                });
            }

            var targetUrl = $"{route.DestinationUrl}/api/{path}";

            var client = _httpClientFactory.CreateClient();

            foreach (var header in Request.Headers)
            {
                if (header.Key.Equals("Content-Type", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value.ToArray());
            }

            HttpResponseMessage response = null;
            HttpContent content = null;

            if (Request.ContentLength > 0)
            {
                using var reader = new StreamReader(Request.Body);
                var bodyText = await reader.ReadToEndAsync();

                var payloadDTO = System.Text.Json.JsonSerializer.Deserialize<PayloadDTO>(bodyText,
                        new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (string.IsNullOrEmpty(payloadDTO?.Data))
                {
                    return BadRequest(new ServiceResponse<object>
                    {
                        Success = false,
                        Message = "Data cannot be empty",
                        Data = null
                    });
                }

                var rawDecryptedString = _crypService.Decrypt(payloadDTO.Data);

                var cleanJsonString = rawDecryptedString.Replace("\0", string.Empty).Trim();

                content = new StringContent(cleanJsonString, System.Text.Encoding.UTF8, "application/json");
            }



            switch (Request.Method.ToUpper())
            {
                case "GET":
                    response = await client.GetAsync(targetUrl);
                    break;

                case "POST":
                    response = await client.PostAsync(targetUrl, content);
                    break;

                case "PUT":
                    response = await client.PutAsync(targetUrl, content);
                    break;

                default:
                    return BadRequest(new ServiceResponse<object>
                    {
                        Success = false,
                        Message = $"Method {Request.Method} not supported by Gateway."
                    });
            }

            var resultString = await response.Content.ReadAsStringAsync();

            var encryptedResponseString = _crypService.Encrypt(resultString);

            if (response.IsSuccessStatusCode)
            {
                return Ok(new ServiceResponse<object>
                {
                    Success = true,
                    Data = encryptedResponseString
                });
            }
            else
            {
                return StatusCode((int)response.StatusCode, resultString);
            }
        }
        
    }
}
