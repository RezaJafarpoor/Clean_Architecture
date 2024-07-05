using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.CQRS.RestaurantsCommands.DeleteRestaurantCommand;

public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger,
    IRestaurantRepository repository) : IRequestHandler<RestaurantsCommands.DeleteRestaurantCommand.DeleteRestaurantCommand>
{
    public async Task Handle(RestaurantsCommands.DeleteRestaurantCommand.DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting Restaurant by id {@Restaurant.Id}",request.Id);
        var restaurant = await repository.GetByIdAsync(request.Id);
        if (restaurant is null)
        {
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        }

        await repository.DeleteAsync(restaurant);
        
    }
}