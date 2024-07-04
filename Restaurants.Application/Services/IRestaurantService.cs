using Restaurants.Application.DTOs;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Services;

public interface IRestaurantService
{
    Task<IEnumerable<RestaurantDTO?>> GetAllRestaurants();
    Task<RestaurantDTO?> GetRestaurantById(int id);
    Task<int> CreateRestaurant(CreateRestaurantDTO restaurantDto);
}