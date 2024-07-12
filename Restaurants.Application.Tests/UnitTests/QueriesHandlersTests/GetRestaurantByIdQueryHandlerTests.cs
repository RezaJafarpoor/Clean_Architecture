using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Restaurants.Application.Common;
using Restaurants.Application.CQRS.Restaurants.RestaurantsQueries.GetRestaurantByIdQuery;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Tests.UnitTests.QueriesHandlersTests;

public class GetRestaurantByIdQueryHandlerTests
{
    private readonly GetRestaurantByIdQueryHandler _sut;

    private readonly ILoggerAdapter<GetRestaurantByIdQueryHandler> _loggerAdapter =
        Substitute.For<ILoggerAdapter<GetRestaurantByIdQueryHandler>>();

    private readonly IRestaurantRepository _restaurantRepository = Substitute.For<IRestaurantRepository>();

    public GetRestaurantByIdQueryHandlerTests()
    {
        _sut = new GetRestaurantByIdQueryHandler(_loggerAdapter, _restaurantRepository);
    }

    [Fact]
    public async void Query_ShouldReturnRestaurantDto_WhenRestaurantExist()
    {
        // Arrange
        var id = 1;
        var query = new GetRestaurantByIdQuery(id);
        var restaurant = new Restaurant()
        {
            Category = "test",
            Description = "test",
            Id = id,
            HasDelivery = true,
            Name = "test",
            Dishes = []
        };
        _restaurantRepository.GetByIdAsync(id).Returns(restaurant);

        // Act
        var result = await _sut.Handle(query, new CancellationToken());
        
        // Assert
        result.Id.Should().Be(id);
    }
    [Fact]
    public async void Query_ShouldThrowsNotFoundException_WhenRestaurantDoesntExist()
    {
        // Arrange
        var id = 1;
        var exception = new NotFoundException(nameof(Restaurant), id.ToString());
        var query = new GetRestaurantByIdQuery(id);
        
        _restaurantRepository.GetByIdAsync(id).ReturnsNull();

        // Act
        var result = async () => await _sut.Handle(query, new CancellationToken());
        
        // Assert
        await result.Should().ThrowAsync<NotFoundException>();

    }
}