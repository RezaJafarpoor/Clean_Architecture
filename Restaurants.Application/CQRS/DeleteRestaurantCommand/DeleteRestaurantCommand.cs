using MediatR;

namespace Restaurants.Application.CQRS.DeleteRestaurantCommand;

public class DeleteRestaurantCommand(int id) : IRequest
{
    public int Id { get; set; } = id;
    
}