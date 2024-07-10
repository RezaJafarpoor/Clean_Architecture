using FluentAssertions;
using NSubstitute;
using Restaurants.Application.CQRS.Dishes.DishesCommands.DeleteDishByIdCommand;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Tests.UnitTests.CommandHandlersTests;

public class DeleteDishByIdCommandHandlerTests
{
    private readonly DeleteDishByIdCommandHandler _sut;
    private readonly IRestaurantRepository _restaurantRepository = Substitute.For<IRestaurantRepository>();

    private readonly IRestaurantAuthorizationService _authorizationService =
        Substitute.For<IRestaurantAuthorizationService>();

    private readonly IDishesRepository _dishesRepository = Substitute.For<IDishesRepository>();

    private readonly ILoggerAdapter<DeleteDishByIdCommandHandler> _loggerAdapter =
        Substitute.For<ILoggerAdapter<DeleteDishByIdCommandHandler>>();

    public DeleteDishByIdCommandHandlerTests()
    {
        _sut = new DeleteDishByIdCommandHandler(_loggerAdapter, _dishesRepository, _authorizationService,
            _restaurantRepository);
    }

    [Fact]
    public async Task DelteDishById_ShouldReturnsTrue_WhenDishIsDeleted()
    {
        // Arrange
        var restaurantId = 1;
        var restaurant = new Restaurant
        {
            Id = 0,
            Name = null,
            Description = null,
            Category = null,
            HasDelivery = false,
            ContactEmail = null,
            ContactNumber = null,
            Address = null,
            Dishes = null,
            Owner = null,
            OwnerId = null
        };
        var dishId = 2;
        var command = new DeleteDishByIdCommand(restaurantId, dishId);
        _restaurantRepository.GetByIdAsync(restaurantId).Returns(new Restaurant());
        _dishesRepository.DeleteDishByIdAsync(restaurantId, dishId).Returns(true);
        _authorizationService.Authorize(Arg.Do<Restaurant>(r => restaurant =r), ResourceOperations.Delete).Returns(true);
        // Act
        var result =await _sut.Handle(command, new CancellationToken());
        // Assert
        result.Should().BeTrue();
    }
    [Fact]
    public async Task DelteDishById_ShouldReturnsFalse_WhenDishIsNotDeleted()
    {
        // Arrange
        var restaurantId = 1;
        var restaurant = new Restaurant
        {
            Id = 0,
            Name = null,
            Description = null,
            Category = null,
            HasDelivery = false,
            ContactEmail = null,
            ContactNumber = null,
            Address = null,
            Dishes = null,
            Owner = null,
            OwnerId = null
        };
        var dishId = 2;
        var command = new DeleteDishByIdCommand(restaurantId, dishId);
        _restaurantRepository.GetByIdAsync(restaurantId).Returns(new Restaurant());
        _dishesRepository.DeleteDishByIdAsync(restaurantId, dishId).Returns(false);
        _authorizationService.Authorize(Arg.Do<Restaurant>(r => restaurant =r), ResourceOperations.Delete).Returns(true);
        // Act
        var result =await _sut.Handle(command, new CancellationToken());
        // Assert
        result.Should().BeFalse();
    }
    [Fact]
    public async Task DelteDishById_ShouldLogMessage_WhenInvoked()
    {
        // Arrange
        var restaurant = new Restaurant
        {
            Id = 0,
            Name = null,
            Description = null,
            Category = null,
            HasDelivery = false,
            ContactEmail = null,
            ContactNumber = null,
            Address = null,
            Dishes = null,
            Owner = null,
            OwnerId = null
        };
        var command = new DeleteDishByIdCommand(1,2);
        
        _restaurantRepository.GetByIdAsync(1).Returns(new Restaurant());
        _dishesRepository.DeleteDishByIdAsync(1, 2).Returns(false);
        _authorizationService.Authorize(Arg.Do<Restaurant>(r => restaurant = r), ResourceOperations.Delete).Returns(true);

        // Act
        await _sut.Handle(command, new CancellationToken());
        
        // Assert
        _loggerAdapter.Received(1).LogWarning(Arg.Is("Deleting Dish with {@dishId} from restaurant with id {@restaurantId}"),
            Arg.Is(2), Arg.Is(1));
    }

}