using CoffeeMachineAPI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachineAPI.Infrastructure.Options
{
    public class WeatherServiceOptions : IWeatherServiceOptions
    {
        public string ApiKey { get; set; }
        public string Location { get; set; }
    }
}
