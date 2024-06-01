using CoffeeMachineAPI.Application.Services;
using CoffeeMachineAPI.Application.Utilities.DateTimeProvider;
using CoffeeMachineAPI.Configuration;
using CoffeeMachineAPI.Domain.Entities;
using CoffeeMachineAPI.Application.Interfaces;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ResolveDependencies();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "CoffeeMachine API",
        Version = "v1"
    });
});

//cross origins
builder.CorsDomainPolicy();

builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.AllCorsDomainPolicy();

app.Run();
