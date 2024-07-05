using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.CQRS.CreateDishCommand;

public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
    IRestaurantRepository restaurantRepository,
    IDishesRepository dishesRepository) : IRequestHandler<CreateDishCommand>
{
    public async Task Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new dish: {@Dish}",request);
        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
        if (restaurant is null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        
        var dish = request.FromEntity(request);
        await dishesRepository.CreateAsync(dish);
    }
}