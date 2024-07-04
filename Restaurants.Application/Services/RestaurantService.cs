using Microsoft.Extensions.Logging;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Services;

internal class RestaurantService(IRestaurantRepository repository,ILogger<RestaurantService> logger) : IRestaurantService
{
    public async Task<IEnumerable<RestaurantDTO?>> GetAllRestaurants()
    {
        logger.LogInformation($"Getting All Restaurants");
        var restaurants = await repository.GetAllAsync();
        var restaurantsDto = restaurants.Select(RestaurantDTO.FromEntity);
        return restaurantsDto;
    }

    public async Task<RestaurantDTO?> GetRestaurantById(int id)
    {
        logger.LogInformation($"Getting  Restaurant By Id");
        var restaurant = await repository.GetByIdAsync(id);
        var restaurantDto = RestaurantDTO.FromEntity(restaurant);

        return restaurantDto;
    }

    public Task<int> CreateRestaurant(CreateRestaurantDTO restaurantDto)
    {
        logger.LogInformation($"Creating a Restaurant");
        var restaurant = CreateRestaurantDTO.FromEntity(restaurantDto);
        var id = repository.Create(restaurant);
        return id;
    }
}