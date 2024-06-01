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

namespace CoffeeMachineAPI.Application.Services
{
    public class CoffeeMachineService : ICoffeeMachineService
    {
        private readonly CoffeeMachine _coffeeMachine;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CoffeeMachineService(CoffeeMachine coffeeMachine, IDateTimeProvider dateTimeProvider)
        {
            _coffeeMachine = coffeeMachine;
            _dateTimeProvider = dateTimeProvider;
        }
        public async Task<(string message, DateTime prepared)> BrewCoffeeAsync()
        {
            var prepared = _dateTimeProvider.Now;
            var message = "Your piping hot coffee is ready";

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

            return (message, prepared);
            
        }
    }
}
