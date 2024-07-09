using FluentAssertions;
using Restaurants.Application.CQRS.Restaurants.RestaurantsCommands.RestaurantCreateCommand;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Tests.UnitTests.MappingTests;

public class CreateRestaurantCommandTests
{


    [Fact]
    public void FromEntity_ShouldReturnARestaurant_WhenCommandExist()
    {
        
        // Arrange
        var command = new CreateRestaurantCommand
        {
            Name= "restaurant", 
            Description = "description",
            Category = "Italian",
            ContactEmail = "test@test.com",
            City = "city",
            PostalCode = "12-345",
            Street = "street",
            HasDelivery = true,
            Id = 1
        };
        
        // Act
        var restaurant = CreateRestaurantCommand.FromEntity(command);
        

        // Assert
        restaurant.Should().BeOfType<Restaurant>();
        restaurant.Address.Should().BeOfType<Address>();

    }
}