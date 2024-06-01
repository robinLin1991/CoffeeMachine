using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachineAPI.Application.Interfaces
{
    public interface IWeatherServiceOptions
    {
        public string ApiKey { get; set; }
        public string Location { get; set; }
    }
}
