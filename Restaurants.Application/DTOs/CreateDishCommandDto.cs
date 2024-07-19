using Restaurants.Application.CQRS.Dishes.DishesCommands.CreateDishCommand;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.DTOs;

public static class CreateDishCommandDto
{
    public static Dish ToDish(this CreateDishCommand extension, CreateDishCommand command)
    {
        
            return new Dish()
            {   RestaurantId = command.RestaurantId,
                Name = command.Name,
                Description = command.Description,
                Price = command.Price,
                KiloCalories = command.KiloCalories,
            };
        }
    
}