using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirements;

public class CreateMultipleRequirement(int  minimumRestaurantCreated ) : IAuthorizationRequirement
{
    public int MinimumRestaurantCreated { get; } = minimumRestaurantCreated;
}