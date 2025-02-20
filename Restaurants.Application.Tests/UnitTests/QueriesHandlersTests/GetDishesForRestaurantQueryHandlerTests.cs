﻿using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Restaurants.Application.Common;
using Restaurants.Application.CQRS.Dishes.DishesQueries.GetDishByIdQuery;
using Restaurants.Application.CQRS.Dishes.DishesQueries.GetDishesForRestaurantQuery;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Tests.UnitTests.QueriesHandlersTests;

public class GetDishesForRestaurantQueryHandlerTests
{
    private readonly GetDishesForRestaurantQueryHandler _sut;

    private readonly ILoggerAdapter<GetDishesForRestaurantQueryHandler> _loggerAdapter =
        Substitute.For<ILoggerAdapter<GetDishesForRestaurantQueryHandler>>();

    private readonly IRestaurantRepository _restaurantRepository = Substitute.For<IRestaurantRepository>();

    public GetDishesForRestaurantQueryHandlerTests()
    {
        _sut = new GetDishesForRestaurantQueryHandler(_loggerAdapter, _restaurantRepository);
    }

    [Fact]
    public async void Query_ShouldReturnIEnumerableOfDishDto_whenRestaurantExist()
    {
        // Arrange
        var dish = new Dish()
        {
            Id = 1,
            Name = "test dish",
            RestaurantId = 1,
            Price = 23,
            Description = "tess"
        };
        var restaurant = new Restaurant()
        {
            Id = 1,
            Name = "test Restaurant",
            Dishes =
            {
                dish
            }
        };
        
        _restaurantRepository.GetByIdAsync(restaurant.Id).Returns(restaurant);

        // Act
        var expected = await _sut.Handle(new GetDishesForRestaurantQuery(1), new CancellationToken());
        var result = expected.FirstOrDefault();
        
        // Assert
        expected.Should().BeEquivalentTo(restaurant.Dishes);
        expected.Should().AllBeOfType<DishDTO>();
        result!.Name.Should().Be(dish.Name);
        
    }

    [Fact]
    public async void Query_ShouldThrowsNotFoundException_WhenRestaurantNotExist()
    {
        // Arrange
        const int id = 1;
        var query = new GetDishesForRestaurantQuery(id);
        var exception = new NotFoundException(nameof(Restaurant), id.ToString());
        _restaurantRepository.GetByIdAsync(id).ReturnsNull();

        // Act
        var result =async () => await _sut.Handle(query, new CancellationToken());
        // Assert
        await result.Should().ThrowAsync<NotFoundException>();
    }
}