using E_Commerce.API.Middlewares;
using E_Commerce.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMemoryCache();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register Infrastructure layer services.
builder.Services.InfrastructureConfiguration(builder.Configuration); 
builder.Services.AddAutoMapper(cfg=>cfg.AddMaps(typeof(Program).Assembly));



builder.Services.AddSwaggerGen(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleWare>();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseAuthorization();

app.MapControllers();

app.Run();
