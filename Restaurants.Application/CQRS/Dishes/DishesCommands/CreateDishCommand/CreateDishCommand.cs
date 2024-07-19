using MediatR;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.CQRS.Dishes.DishesCommands.CreateDishCommand;

public class CreateDishCommand : IRequest<int>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int? KiloCalories { get; set; }
    public int  RestaurantId { get; set; }

    
}