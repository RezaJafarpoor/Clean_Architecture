using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IRestaurantAuthorizationService
{
    public bool Authorize(Restaurant restaurant, ResourceOperations operation);
}