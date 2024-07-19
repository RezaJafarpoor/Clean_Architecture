using Restaurants.Domain.Entities;

namespace Restaurants.Application.DTOs;

public static class DishDtoMapper
{
     public static DishDTO? ToDishDto(this DishDTO dishDto, Dish? dish)
     {
          if (dish == null) return null;
          return new DishDTO()
          {
               Id = dish.Id,
               Name = dish.Name,
               Description = dish.Description,
               Price = dish.Price,
               KiloCalories = dish.KiloCalories,
               RestaurantId = dish.RestaurantId
          };
     }
}