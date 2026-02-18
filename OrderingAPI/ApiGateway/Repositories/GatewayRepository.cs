using ApiGateway.Interfaces;
using ApiGateway.Models;
using MySql.Data.MySqlClient;

namespace ApiGateway.Repositories
{
    public class GatewayRepository : IGatewayRepository
    {
        private readonly string _connectionString;

        public GatewayRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<ApiRoute> GetRouteByPath(string path)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("SELECT * FROM api_routes WHERE path = @path", conn);
            cmd.Parameters.AddWithValue("@path", path);

            using var reader = await cmd.ExecuteReaderAsync();

            if(await reader.ReadAsync())
            {
                return new ApiRoute
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Path = reader["path"].ToString(),
                    DestinationUrl = reader["destination_url"].ToString()
                };
            }

            return null;
        }
    }
}
