using ApiGateway.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderingAPI.Shared.Models;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/{*path}")]
    public class GatewayController : ControllerBase
    {
        private readonly IGatewayRepository _repository;
        private readonly IHttpClientFactory _httpClientFactory;

        public GatewayController(IGatewayRepository repository, IHttpClientFactory httpClientFactory)
        {
            _repository = repository;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> HandleGet(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return BadRequest(new ServiceResponse<object>
                {
                    Success = false,
                    Message = "The path field is required"
                });
            }

            var serviceKey = path.Split('/')[0];
            var route = await _repository.GetRouteByPath(serviceKey);

            if(route == null)
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
                client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value.ToArray());
            }

            var response = await client.GetAsync(targetUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, errorContent);
            }
        }

       
    }
}
