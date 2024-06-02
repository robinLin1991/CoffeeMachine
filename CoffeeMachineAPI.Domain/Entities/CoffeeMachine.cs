using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachineAPI.Domain.Entities
{
    public class CoffeeMachine
    {
        public int BrewCount { get; set; } = 0;
        public bool IsOutOfCoffee => BrewCount !=0 && BrewCount % 5 == 0;
        
        public bool IsFIrstOfApril(DateTime date)
        {
            return date.Month == 4 && date.Day == 1;
        }
    }
}
