using MediatR;

namespace Restaurants.Application.CQRS.RestaurantsCommands.DeleteRestaurantCommand;

public class DeleteRestaurantCommand(int id) : IRequest
{
    public int Id { get; set; } = id;
    
}