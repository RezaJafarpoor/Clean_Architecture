using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.CQRS.Dishes.DishesCommands.CreateDishCommand;

public class CreateDishCommandHandler(ILoggerAdapter<CreateDishCommandHandler> logger,
    IRestaurantRepository restaurantRepository,
    IDishesRepository dishesRepository,
    IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<CreateDishCommand, int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new dish: {@Dish}",request);
        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
        if (restaurant is null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperations.Create))
        {
            throw new ForbidException();
        }
        var dish = request.ToDish(request);
        var id =await dishesRepository.CreateAsync(dish);
        return id;
    }
}