using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.CQRS.Restaurants.RestaurantsQueries.GetRestaurantByIdQuery;

public class GetRestaurantByIdQueryHandler(ILoggerAdapter<GetRestaurantByIdQueryHandler> logger,
    IRestaurantRepository repository) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
{
    public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting  Restaurant By {@Restaurant}",request);
        var restaurant = await repository.GetByIdAsync(request.Id)
                         ?? throw new NotFoundException(nameof(Restaurant),request.Id.ToString());
        var restaurantDto = request.ToRestaurantDto(restaurant);

        return restaurantDto;
    }
}