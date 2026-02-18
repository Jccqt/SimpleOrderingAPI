using ApiGateway.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderingAPI.Shared.Models;
using Org.BouncyCastle.Asn1.Ocsp;

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

        [AcceptVerbs("GET", "POST", "PUT")]
        public async Task<IActionResult> HandleRequests(string path)
        {
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
                if (header.Key.Equals("Content-Type", StringComparison.OrdinalIgnoreCase)) continue;
                client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value.ToArray());
            }

            HttpResponseMessage response = null;
            HttpContent content = null;

            if (Request.ContentLength > 0)
            {
                content = new StreamContent(Request.Body);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(Request.ContentType ?? "application/json");
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

            if (response.IsSuccessStatusCode)
            {
                return Content(resultString, "application/json");
            }
            else
            {
                return StatusCode((int)response.StatusCode, resultString);
            }
        }
        
    }
}
