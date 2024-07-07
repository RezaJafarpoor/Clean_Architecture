using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.UserContext;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.CQRS.Restaurants.RestaurantsCommands.RestaurantCreateCommand;

public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger,
    IRestaurantRepository repository, IUserContext userContext) : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        logger.LogInformation("User {@user} Creating a new Restaurant {@Restaurant}",currentUser.Email, request);
        var restaurant = CreateRestaurantCommand.FromEntity(request);
        restaurant.OwnerId = currentUser.Id;
        var id = await repository.Create(restaurant);
        return id;
    }
}