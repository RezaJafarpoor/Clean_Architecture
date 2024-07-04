using Restaurants.Domain.Entities;

namespace Restaurants.Application.DTOs;

public class DishDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int RestaurantId { get; set; }
    public int? KiloCalories { get; set; }

    public static DishDTO? FromEntity(Dish? dish)
    {
        if (dish == null) return null;
        return new DishDTO()
        {
            Id = dish.Id,
            Name = dish.Name,
            Description = dish.Description,
            Price = dish.Price,
            KiloCalories = dish.KiloCalories
        };
    }
}