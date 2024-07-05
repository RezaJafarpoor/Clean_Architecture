using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.CQRS.Restaurants.RestaurantsQueries.GetRestaurantByIdQuery;

public class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger,
    IRestaurantRepository repository) : IRequestHandler<Restaurants.RestaurantsQueries.GetRestaurantByIdQuery.GetRestaurantByIdQuery, RestaurantDTO>
{
    public async Task<RestaurantDTO> Handle(Restaurants.RestaurantsQueries.GetRestaurantByIdQuery.GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting  Restaurant By {@Restaurant}",request);
        var restaurant = await repository.GetByIdAsync(request.Id)
                         ?? throw new NotFoundException(nameof(Restaurant),request.Id.ToString());
        var restaurantDto = RestaurantDTO.FromEntity(restaurant);

        return restaurantDto;
    }
}