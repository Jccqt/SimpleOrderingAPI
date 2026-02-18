using ApiGateway.Interfaces;
using ApiGateway.Repositories;
using OrderingAPI.Shared.MiddleWare;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IGatewayRepository, GatewayRepository>();

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