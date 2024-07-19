using Restaurants.Application.CQRS.Restaurants.RestaurantsCommands.UpdateRestaurantCommand;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.DTOs;

public static class UpdateRestaurantCommandDto
{
    public static void ToRestaurant(this UpdateRestaurantCommand command, UpdateRestaurantCommand source,Restaurant destination )
    {
        
            destination.Name = source.Name;
            destination.Description = source.Description;
            destination.HasDelivery = source.HasDelivery;

        
    }
}