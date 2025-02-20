﻿using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.CQRS.Restaurants.RestaurantsCommands.DeleteRestaurantCommand;

public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger,
    IRestaurantRepository repository,
    IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<Restaurants.RestaurantsCommands.DeleteRestaurantCommand.DeleteRestaurantCommand>
{
    public async Task Handle(Restaurants.RestaurantsCommands.DeleteRestaurantCommand.DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting Restaurant by id {@Restaurant.Id}",request.Id);
        var restaurant = await repository.GetByIdAsync(request.Id);
        if (restaurant is null)
        {
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        }

        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperations.Delete))
        {
            throw new ForbidException();
        }

        await repository.DeleteAsync(restaurant);
        
    }
}