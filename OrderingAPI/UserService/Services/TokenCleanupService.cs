using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;      
using Microsoft.Extensions.Configuration;

namespace UserService.Services
{
    public class TokenCleanupService : BackgroundService
    {
        private readonly string _connectionString;
        private readonly ILogger<TokenCleanupService> _logger;

        public TokenCleanupService(IConfiguration configuration, ILogger<TokenCleanupService> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Token Cleanup Service Started");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Cleaning up expired tokens.");

                    await DeleteExpiredTokens();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while cleaning expired tokens.");
                }

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }

        private async Task DeleteExpiredTokens()
        {
            using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand("DELETE FROM user_sessions WHERE expires < NOW()", conn);

            int rowsAffected = await cmd.ExecuteNonQueryAsync();

            _logger.LogInformation($"Cleanup complete. Deleted {rowsAffected} expired tokens");
        }
    }
}
