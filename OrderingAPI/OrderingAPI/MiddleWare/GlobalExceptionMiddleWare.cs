using OrderingAPI.Interfaces;
using OrderingAPI.Models;
using System.Net;
using System.Text.Json;

namespace OrderingAPI.MiddleWare
{
    public class GlobalExceptionMiddleWare
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleWare(RequestDelegate next)
        {
            _next = next; 
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var errorRepo = scope.ServiceProvider.GetRequiredService<IErrorLogRepository>();
                    await HandleExceptionAsync(context, ex, errorRepo);
                }
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception, IErrorLogRepository errorRepo)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string traceID = Guid.NewGuid().ToString();

            await errorRepo.LogError(traceID, exception.Message, exception.ToString());

            var response = new ServiceResponse<string>
            {
                Success = false,
                Message = "An internal error occured.",
                TraceID = traceID
            };

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}
