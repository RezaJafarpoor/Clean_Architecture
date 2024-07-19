using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using System.Linq.Expressions;

namespace Restaurants.Application.CQRS.Dishes.DishesQueries.GetDishesForRestaurantQuery;

public class GetDishesForRestaurantQueryHandler(ILoggerAdapter<GetDishesForRestaurantQueryHandler> logger,
    IRestaurantRepository repository) : IRequestHandler<Dishes.DishesQueries.GetDishesForRestaurantQuery.GetDishesForRestaurantQuery, IEnumerable<DishDTO>>
{
    public async Task<IEnumerable<DishDTO>> Handle(Dishes.DishesQueries.GetDishesForRestaurantQuery.GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
    {
        var restaurant = await repository.GetByIdAsync(request.RestaurantId);
        if (restaurant is null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        var dishes = request.FromEntity(restaurant);
        var dishDto = dishes.Select(new DishDTO().ToDishDto);
        return dishDto;
    }
}