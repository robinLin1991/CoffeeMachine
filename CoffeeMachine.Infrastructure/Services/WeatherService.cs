using CoffeeMachineAPI.Application.Interfaces;
using CoffeeMachineAPI.Domain.Enums;
using CoffeeMachineAPI.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoffeeMachineAPI.Infrastructure.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IWeatherServiceOptions _options;

        public WeatherService(HttpClient httpClient, IOptions<WeatherServiceOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task<double> GetCurrentTemperatureAsync()
        {
            var response = await _httpClient.GetStringAsync($"data/2.5/weather?q={_options.Location}&appid={_options.ApiKey}&units=metric");
            var data = JObject.Parse(response);
            return data["main"]["temp"].Value<double>();
        }
    }
}
