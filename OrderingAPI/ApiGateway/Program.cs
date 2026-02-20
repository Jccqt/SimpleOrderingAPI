using ApiGateway.Interfaces;
using ApiGateway.Repositories;
using OrderingAPI.Shared.Interfaces;
using OrderingAPI.Shared.MiddleWare;
using OrderingAPI.Shared.Repositories;
using OrderingAPI.Shared.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IErrorLogRepository, ErrorLogRepository>();
builder.Services.AddScoped<IGatewayRepository, GatewayRepository>();
builder.Services.AddSingleton<AESCryptService>();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleWare>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();