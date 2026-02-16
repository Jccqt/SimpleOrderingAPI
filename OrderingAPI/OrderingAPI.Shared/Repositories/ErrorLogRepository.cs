using MySql.Data.MySqlClient;
using OrderingAPI.Shared.Interfaces;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace OrderingAPI.Shared.Repositories
{
    public class ErrorLogRepository : IErrorLogRepository
    {
        private readonly string _connectionString;

        public ErrorLogRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task LogError(string traceID, string message, string stackTrace)
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("LogError", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_trace_id", traceID);
            cmd.Parameters.AddWithValue("@p_message", message);
            cmd.Parameters.AddWithValue("@p_stack_trace", stackTrace);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}
