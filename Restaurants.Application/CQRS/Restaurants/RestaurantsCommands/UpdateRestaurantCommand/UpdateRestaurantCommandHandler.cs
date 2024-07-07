using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.CQRS.Restaurants.RestaurantsCommands.UpdateRestaurantCommand;

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommand> logger,
    IRestaurantRepository repository,
    IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating Restaurant {@Restaurant} with Id {@Id}",request, request.Id);
        var restaurant = await repository.GetByIdAsync(request.Id);
        if (restaurant is null)
        {
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        }
        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperations.Update))
        {
            throw new ForbidException();
        }

        request.FromEntity(request,restaurant);
        await repository.SaveChanges();
        


    }
}