using CoffeeMachineAPI.Application.Interfaces;
using CoffeeMachineAPI.Application.Services;
using CoffeeMachineAPI.Application.Utilities.DateTimeProvider;
using CoffeeMachineAPI.Domain.Entities;
using CoffeeMachineAPI.Domain.Enums;
using Moq;

namespace CoffeeMachineAPI.Tests
{
    public class CoffeeMachineServiceTests
    {
        [Fact]
        public async Task BrewCoffee_ReturnsServiceUnavailableOnFifthCall()
        {
            // Arrange
            var coffeeMachine = new CoffeeMachine();
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();
            var weatherService = new Mock<IWeatherService>();

            dateTimeProviderMock.Setup(p => p.Now).Returns(DateTime.UtcNow);
            weatherService.Setup(w => w.GetCurrentTemperatureAsync()).ReturnsAsync(25.0);

            var service = new CoffeeMachineService(coffeeMachine, dateTimeProviderMock.Object, weatherService.Object);

            // Act
            for (int i = 0; i < 4; i++)
            {
                await service.BrewCoffeeAsync();
            }

            // Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () => await service.BrewCoffeeAsync());
            Assert.Equal(StatusResults.ServiceUnavailable, ex.Message);
        }

        [Fact]
        public async Task BrewCoffee_ReturnsMessageAndTime()
        {
            // Arrange
            var coffeeMachine = new CoffeeMachine();
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();
            var weatherService = new Mock<IWeatherService>();

            dateTimeProviderMock.Setup(p => p.Now).Returns(DateTime.UtcNow);
            weatherService.Setup(w => w.GetCurrentTemperatureAsync()).ReturnsAsync(25.0);

            var service = new CoffeeMachineService(coffeeMachine, dateTimeProviderMock.Object, weatherService.Object);

            // Act
            var result = await service.BrewCoffeeAsync();

            // Assert
            Assert.Equal(CoffeeMessages.HOT_COFFEE_MSG, result.message);
            Assert.Equal(DateTime.UtcNow.Date, result.prepared.Date);
        }

        [Fact]
        public async Task BrewCoffee_ReturnsTeapotOnFirstOfApril()
        {
            // Arrange
            var coffeeMachine = new CoffeeMachine();
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();
            var weatherService = new Mock<IWeatherService>();
            var aprilFirst = new DateTime(DateTime.UtcNow.Year, 4, 1);

            dateTimeProviderMock.Setup(p => p.Now).Returns(aprilFirst);
            weatherService.Setup(w => w.GetCurrentTemperatureAsync()).ReturnsAsync(25.0);

            var service = new CoffeeMachineService(coffeeMachine, dateTimeProviderMock.Object, weatherService.Object);

            // Act 
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () => await service.BrewCoffeeAsync());

            //Assert
            Assert.Equal(StatusResults.Teapot, ex.Message);
        }


        [Fact]
        public async Task BrewCoffee_ReturnsIcedCoffeeWhenTemperatureIsAbove30()
        {
            // Arrange
            var coffeeMachine = new CoffeeMachine();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var weatherService = new Mock<IWeatherService>();
            dateTimeProvider.Setup(p => p.Now).Returns(DateTime.UtcNow);
            weatherService.Setup(w => w.GetCurrentTemperatureAsync()).ReturnsAsync(35.0);
            var service = new CoffeeMachineService(coffeeMachine, dateTimeProvider.Object, weatherService.Object);

            // Act
            var result = await service.BrewCoffeeAsync();

            // Assert
            Assert.Equal(CoffeeMessages.ICED_COFFEE_MSG, result.message);
            Assert.Equal(DateTime.UtcNow.Date, result.prepared.Date);
        }
    }
}