using CoffeeMachineAPI.Application.Interfaces;
using CoffeeMachineAPI.Application.Services;
using CoffeeMachineAPI.Application.Utilities.DateTimeProvider;
using CoffeeMachineAPI.Domain.Entities;

namespace CoffeeMachineAPI.Configuration
{
    public static class DependencyInjectionConfig
    {
        //Define Dependency Injections and configs
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddSingleton<CoffeeMachine>();
            services.AddScoped<ICoffeeMachineService, CoffeeMachineService>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            return services;
        }
    }
}
