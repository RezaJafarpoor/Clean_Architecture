using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.UserContext;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirements;

public class CreateMultipleRequirementHandler(IRestaurantRepository restaurantRepository,
    IUserContext userContext) : AuthorizationHandler<CreateMultipleRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreateMultipleRequirement requirement)
    {
        var currentUser = userContext.GetCurrentUser();
        var restaurants = await restaurantRepository.GetAllAsync();
        var userRestaurantsCreated = restaurants.Count(r => r.OwnerId == currentUser!.Id);
        if (userRestaurantsCreated >= requirement.MinimumRestaurantCreated) 
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}