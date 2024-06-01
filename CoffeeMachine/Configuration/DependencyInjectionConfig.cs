using CoffeeMachineAPI.Application.Interfaces;
using CoffeeMachineAPI.Application.Services;
using CoffeeMachineAPI.Application.Utilities.DateTimeProvider;
using CoffeeMachineAPI.Domain.Entities;
using CoffeeMachineAPI.Infrastructure.Options;
using CoffeeMachineAPI.Infrastructure.Services;
using Microsoft.Extensions.Options;

namespace CoffeeMachineAPI.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddSingleton<CoffeeMachine>();
            services.AddScoped<ICoffeeMachineService, CoffeeMachineService>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IWeatherService, WeatherService>();

            // Configure WeatherService
            services.AddHttpClient<IWeatherService, WeatherService>(client =>
            {
                client.BaseAddress = new Uri("http://api.openweathermap.org/");
            });

            // Bind configuration sections to options classes
            services.Configure<WeatherServiceOptions>(builder.Configuration.GetSection("WeatherConfigs"));
            services.AddSingleton<IWeatherServiceOptions>(sp => sp.GetRequiredService<IOptions<WeatherServiceOptions>>().Value);

            return services;
        }
    }
}
