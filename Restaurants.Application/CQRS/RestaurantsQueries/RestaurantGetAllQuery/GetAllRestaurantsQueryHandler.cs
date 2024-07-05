﻿using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.CQRS.RestaurantsQueries.RestaurantGetAllQuery;

public class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger,
    IRestaurantRepository repository) : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDTO>>
{
    public async Task<IEnumerable<RestaurantDTO>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Getting All Restaurants");
        var restaurants = await repository.GetAllAsync();
        var restaurantsDto = restaurants.Select(RestaurantDTO.FromEntity);
        return restaurantsDto!;
    }
}