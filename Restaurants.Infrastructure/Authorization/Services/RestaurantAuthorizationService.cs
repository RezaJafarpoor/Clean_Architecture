using Microsoft.Extensions.Logging;
using Restaurants.Application.UserContext;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Services;

public class RestaurantAuthorizationService(IUserContext userContext, ILogger<RestaurantAuthorizationService> logger) : IRestaurantAuthorizationService
{
    public bool Authorize(Restaurant restaurant, ResourceOperations operation)
    {
        var currentUser = userContext.GetCurrentUser();
        

        if (operation == ResourceOperations.Read || operation == ResourceOperations.Create)
        {
            logger.LogInformation("Create/Read operation. - successful authorization");
            return true;
        }

        if (operation is ResourceOperations.Delete or ResourceOperations.Create or ResourceOperations.Read or ResourceOperations.Update
            && currentUser.IsInRole(UserRoles.Admin))
        {
            logger.LogInformation("Admin User. full access successful authorization");
            return true;
        }

        if (operation is ResourceOperations.Update or ResourceOperations.Delete
            && currentUser.Id == restaurant.OwnerId)
        {
            logger.LogInformation("Restaurant owner. successful authorization");
            return true;
        }

        return false;
        
        
        

    }
}