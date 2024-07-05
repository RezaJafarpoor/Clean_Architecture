using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.CQRS.UpdateRestaurantCommand;

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommand> logger,
    IRestaurantRepository repository) : IRequestHandler<UpdateRestaurantCommand,bool>
{
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating Restaurant {@Restaurant} with Id {@Id}",request, request.Id);
        var restaurant = await repository.GetByIdAsync(request.Id);
        if (restaurant is null)
        {
            return false;
        }

        request.FromEntity(request,restaurant);
        await repository.SaveChanges();
        
        return true;


    }
}