using MediatR;

namespace Restaurants.Application.CQRS.DeleteRestaurantCommand;

public class DeleteRestaurantCommand(int id) : IRequest<bool>
{
    public int Id { get; set; } = id;
    
}