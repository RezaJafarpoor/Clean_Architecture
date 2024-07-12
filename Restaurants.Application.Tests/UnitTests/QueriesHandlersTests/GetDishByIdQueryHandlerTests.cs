using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using Restaurants.Application.Common;
using Restaurants.Application.CQRS.Dishes.DishesQueries.GetDishByIdQuery;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Tests.UnitTests.QueriesHandlersTests;

public class GetDishByIdQueryHandlerTests
{
    private readonly GetDishByIdQueryHandler _sut;

    private readonly ILoggerAdapter<GetDishByIdQueryHandler> _logger =
        Substitute.For<ILoggerAdapter<GetDishByIdQueryHandler>>();

    private readonly IDishesRepository _dishesRepository = Substitute.For<IDishesRepository>();

    public GetDishByIdQueryHandlerTests()
    {
        _sut = new GetDishByIdQueryHandler(_logger, _dishesRepository);
    }

    [Fact]
    public async void Query_ShouldReturnDishDto_WhenDishExist()
    {
        // Arrange
        var restaurantId = 1;
        var dishId = 2;
        var command = new GetDishByIdQuery(restaurantId, dishId);
        var dish = new Dish()
        {
            Id = command.DishId,
            Description = "test description",
            RestaurantId = command.RestaurantId,
            Name = "test dish",
            Price = 1234
        };
        _dishesRepository.GetDishByIdAsync(restaurantId, dishId).Returns(dish);


        // Act
        var expected = await _sut.Handle(command, new CancellationToken());
        // Assert
        expected.RestaurantId.Should().Be(command.RestaurantId);
        expected.Id.Should().Be(command.DishId);
        expected.Name.Should().Be(dish.Name);
        expected.Should().BeOfType<DishDTO>();


    }
    [Fact]
    public async void Command_ShouldThrowsNotFoundException_WhenDishIsNotExist()
    {
        // Arrange
        var excpetion = new NotFoundException(nameof(Dish), 2.ToString());
        var query = new GetDishByIdQuery(1, 2);
        _dishesRepository.GetDishByIdAsync(1, 2).ReturnsNull();

        // Act
        var result = async () => await _sut.Handle(query, new CancellationToken());
        
        // Assert
       await result.Should().ThrowAsync<NotFoundException>();
    }
}