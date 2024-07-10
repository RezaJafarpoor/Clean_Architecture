using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using Restaurants.Application.CQRS.Dishes.DishesCommands.CreateDishCommand;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Tests.UnitTests.CommandHandlersTests;

public class CreateDishCommandHandlerTests
{
    private readonly CreateDishCommandHandler _sut;
    private readonly IRestaurantRepository _restaurantRepository = Substitute.For<IRestaurantRepository>();
    private readonly IDishesRepository _dishesRepository = Substitute.For<IDishesRepository>();

    private readonly IRestaurantAuthorizationService _authorizationService =
        Substitute.For<IRestaurantAuthorizationService>();

    private readonly ILoggerAdapter<CreateDishCommandHandler> _loggerAdapter =
        Substitute.For<ILoggerAdapter<CreateDishCommandHandler>>();

    public CreateDishCommandHandlerTests()
    {
        _sut = new CreateDishCommandHandler(_loggerAdapter,
            _restaurantRepository,
            _dishesRepository,
            _authorizationService);
    }

    [Fact]
    public async Task Command_ShouldReturnId_WhenDishCreated()
    {
        // Arrange
        var command = new CreateDishCommand
        {
            Name = "testDish",
            Description = "test",
            Price = 1,
            KiloCalories = null,
            RestaurantId = 1
        };
        Restaurant restaurant = new Restaurant
        {
            Id = 1,
            Name = null,
            Description = null,
            Category = null,
            HasDelivery = false,
            ContactEmail = null,
            ContactNumber = null,
            Address = null,
            Dishes = null,
            Owner = null,
            OwnerId = Guid.NewGuid().ToString()
        };
        var dishId = 3; 
        _restaurantRepository.GetByIdAsync(command.RestaurantId).Returns(restaurant);
        _authorizationService.Authorize(Arg.Do<Restaurant>(x => restaurant =x), ResourceOperations.Create).Returns(true);
        var dish = command.FromEntity(command);
        dish.Id = dishId;
        _dishesRepository.CreateAsync(Arg.Do<Dish>(d => dish =d)).Returns(dishId);
        // Act

        var result = await _sut.Handle(command, new CancellationToken());

        // Assert
        result.Should().Be(dishId);
    }
    
    [Fact]
    public async Task Command_ShouldLogMessage_WhenDishCreated()
    {
        
        // Arrange
        var command = new CreateDishCommand
        {
            Name = "testDish",
            Description = "test",
            Price = 1,
            KiloCalories = null,
            RestaurantId = 1
        };
        _loggerAdapter.LogInformation("Creating new dish: {@Dish}",command);
        
        // Act
        var result =async () => await _sut.Handle(command, new CancellationToken());
        
        // Assert
        _loggerAdapter.Received(1).LogInformation(Arg.Is("Creating new dish: {@Dish}"), Arg.Is(command));
        
    }
    [Fact]
    public async Task Command_ShouldLogMessageAndNotFoundException_WhenRestaurantIsNull()
    {
        
        // Arrange
        var command = new CreateDishCommand
        {
            Name = "testDish",
            Description = "test",
            Price = 1,
            KiloCalories = null,
            RestaurantId = 1
        };
        var exception = new NotFoundException(nameof(Restaurant), command.RestaurantId.ToString());

        var restaurant = _restaurantRepository.GetByIdAsync(command.RestaurantId).ReturnsNull();
        // Act
        var requestAction = async () => await _sut.Handle(command, new CancellationToken());

        // Assert
        await requestAction.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Restaurant with id {command.RestaurantId.ToString()} doesn't exist");

    }
    [Fact]
    public async Task Command_ShouldLogMessageAndForbid_WhenUserIsNotAuthorized()
    {
        
        // Arrange
        var command = new CreateDishCommand
        {
            Name = "testDish",
            Description = "test",
            Price = 1,
            KiloCalories = null,
            RestaurantId = 1
        };
        var exception = new ForbidException();
        _restaurantRepository.GetByIdAsync(command.RestaurantId).Returns(new Restaurant());
        _authorizationService.Authorize(new Restaurant(), ResourceOperations.Create).Returns(false);

        // Act
        var requestAction = async () => await _sut.Handle(command, new CancellationToken());
        // Assert
        await requestAction.Should().ThrowAsync<ForbidException>();

    }
}