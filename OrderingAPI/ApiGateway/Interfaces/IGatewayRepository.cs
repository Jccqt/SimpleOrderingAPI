using ApiGateway.Models;

namespace ApiGateway.Interfaces
{
    public interface IGatewayRepository
    {
        Task<ApiRoute> GetRouteByPath(string path);
    }
}
