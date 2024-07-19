using Restaurants.Application.CQRS.Restaurants.RestaurantsQueries.RestaurantGetAllQuery;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.DTOs;

public static class GetAllRestaurantQueryDto
{
    public static RestaurantDto? ToRestaurantDto(this GetAllRestaurantsQuery query, Restaurant? restaurant)
    {
        if (restaurant == null) return null;
        return new RestaurantDto()
        {
            Category = restaurant.Category,
            Description = restaurant.Description,
            Id = restaurant.Id,
            HasDelivery = restaurant.HasDelivery,
            Name = restaurant.Name,
            City = restaurant.Address.City,
            Street = restaurant.Address.Street,
            PostalCode = restaurant.Address.PostalCode,
            Dishes = restaurant.Dishes.Select(DishDTO.FromEntity).ToList()
        };
    }
}