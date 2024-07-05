using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.CQRS.Dishes.DishesCommands.CreateDishCommand;

public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
    IRestaurantRepository restaurantRepository,
    IDishesRepository dishesRepository) : IRequestHandler<Dishes.DishesCommands.CreateDishCommand.CreateDishCommand>
{
    public async Task Handle(Dishes.DishesCommands.CreateDishCommand.CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new dish: {@Dish}",request);
        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
        if (restaurant is null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        
        var dish = request.FromEntity(request);
        await dishesRepository.CreateAsync(dish);
    }
}