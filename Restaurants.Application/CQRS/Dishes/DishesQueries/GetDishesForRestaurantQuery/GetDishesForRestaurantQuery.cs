using MediatR;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.CQRS.Dishes.DishesQueries.GetDishesForRestaurantQuery;

public class GetDishesForRestaurantQuery(int restaurantId):IRequest<IEnumerable<DishDTO>>
{
    public int RestaurantId { get; set; } = restaurantId;


    internal IEnumerable<Dish> FromEntity(Restaurant restaurant)
    {
        var dishes= restaurant.Dishes;
        return dishes;
    }
}