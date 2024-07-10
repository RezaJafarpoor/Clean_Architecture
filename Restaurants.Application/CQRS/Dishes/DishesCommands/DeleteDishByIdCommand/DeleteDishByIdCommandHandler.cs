using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.CQRS.Dishes.DishesCommands.DeleteDishByIdCommand;

public class DeleteDishByIdCommandHandler(ILoggerAdapter<DeleteDishByIdCommandHandler> logger,
    IDishesRepository dishesRepository,
    IRestaurantAuthorizationService restaurantAuthorizationService,
    IRestaurantRepository restaurantRepository) : IRequestHandler<DeleteDishByIdCommand, bool>
{
    public async Task<bool> Handle(DeleteDishByIdCommand request, CancellationToken cancellationToken)
    {
        logger.LogWarning("Deleting Dish with {@dishId} from restaurant with id {@restaurantId}", request.DishId, request.RestaurantId);
        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperations.Delete))
        {
            throw new ForbidException();
        }
        var result = await dishesRepository.DeleteDishByIdAsync(request.RestaurantId, request.DishId);
        return result;
    }
}