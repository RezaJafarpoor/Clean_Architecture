using MediatR;

namespace Restaurants.Application.CQRS.Restaurants.RestaurantsCommands.DeleteRestaurantCommand;

public class DeleteRestaurantCommand(int id) : IRequest
{
    public int Id { get; set; } = id;
    
}