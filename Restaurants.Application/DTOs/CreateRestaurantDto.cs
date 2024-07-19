using Restaurants.Application.CQRS.Restaurants.RestaurantsCommands.RestaurantCreateCommand;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.DTOs;

public static class CreateRestaurantCommandDto
{
    public static Restaurant? ToRestaurant(this CreateRestaurantCommand command, CreateRestaurantCommand restaurantCommand)
    {
        return new Restaurant()
        {
            Name = restaurantCommand.Name,
            Description = restaurantCommand.Description,
            Category = restaurantCommand.Category,
            HasDelivery = restaurantCommand.HasDelivery,
            ContactEmail = restaurantCommand.ContactEmail,
            ContactNumber = restaurantCommand.ContactNumber,
            Address = new Address()
            {
                City = restaurantCommand.City,
                Street = restaurantCommand.Street,
                PostalCode = restaurantCommand.PostalCode
            }
        };
    }
}