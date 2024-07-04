using Microsoft.Extensions.Logging;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Services;

internal class RestaurantService(IRestaurantRepository repository,ILogger<RestaurantService> logger) : IRestaurantService
{
    public async Task<IEnumerable<RestaurantDTO?>> GetAllRestaurants()
    {
        var restaurants = await repository.GetAllAsync();
        var restaurantsDto = restaurants.Select(RestaurantDTO.FromEntity);
        logger.LogInformation($"Getting All Restaurants");
        return restaurantsDto;
    }

    public async Task<RestaurantDTO?> GetRestaurantById(int id)
    {
        var restaurant = await repository.GetByIdAsync(id);
        var restaurantDto = RestaurantDTO.FromEntity(restaurant);
        return restaurantDto;
    }
}