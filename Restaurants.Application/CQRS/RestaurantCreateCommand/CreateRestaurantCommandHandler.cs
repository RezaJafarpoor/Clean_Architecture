using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.CQRS.RestaurantCreateCommand;

public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger,
    IRestaurantRepository repository) : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a new Restaurant {@Restaurant}",request);
        var restaurant = CreateRestaurantCommand.FromEntity(request);
        var id = await repository.Create(restaurant);
        return id;
    }
}