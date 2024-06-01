﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachineAPI.Application.Interfaces
{
    public interface IWeatherService
    {
        Task<double> GetCurrentTemperatureAsync();
    }
}
