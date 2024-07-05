﻿using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.CQRS.RestaurantsQueries.GetRestaurantByIdQuery;

public class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger,
    IRestaurantRepository repository) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDTO?>
{
    public async Task<RestaurantDTO?> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Getting  Restaurant By {request.Id}");
        var restaurant = await repository.GetByIdAsync(request.Id);
        var restaurantDto = RestaurantDTO.FromEntity(restaurant);

        return restaurantDto;
    }
}