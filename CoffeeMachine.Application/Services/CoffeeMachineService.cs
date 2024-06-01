using CoffeeMachineAPI.Application.Interfaces;
using CoffeeMachineAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using CoffeeMachineAPI.Domain.Enums;
using CoffeeMachineAPI.Application.Utilities.DateTimeProvider;
using Microsoft.Extensions.Options;

namespace CoffeeMachineAPI.Application.Services
{
    public class CoffeeMachineService : ICoffeeMachineService
    {
        private readonly CoffeeMachine _coffeeMachine;
        private readonly IDateTimeProvider _dateTimeProvider;

        private readonly IWeatherService _weatherService;

        public CoffeeMachineService(CoffeeMachine coffeeMachine, IDateTimeProvider dateTimeProvider, IWeatherService weatherService)
        {
            _coffeeMachine = coffeeMachine;
            _dateTimeProvider = dateTimeProvider;
            _weatherService = weatherService;
        }
        public async Task<(string message, DateTime prepared)> BrewCoffeeAsync()
        {
            var prepared = _dateTimeProvider.Now;
            var message = CoffeeMessages.HOT_COFFEE_MSG;

            _coffeeMachine.BrewCount++;

            if (_coffeeMachine.IsFIrstOfApril(prepared))
            {
                message = StatusResults.Teapot;
                throw new InvalidOperationException(message);
            }

            if (_coffeeMachine.IsOutOfCoffee)
            {
                message = StatusResults.ServiceUnavailable;
                throw new InvalidOperationException(message);
            }

            var currentTemperature = await _weatherService.GetCurrentTemperatureAsync();
            if (currentTemperature > 30)
            {
                message = CoffeeMessages.ICED_COFFEE_MSG;
            }

            return (message, prepared);
            
        }
    }
}
