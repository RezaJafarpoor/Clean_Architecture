using MediatR;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.CQRS.GetDishesForRestaurantQuery;

public class GetDishesForRestaurantQuery(int restaurantId):IRequest<IEnumerable<DishDTO>>
{
    public int restaurantId { get; set; } = restaurantId;
}