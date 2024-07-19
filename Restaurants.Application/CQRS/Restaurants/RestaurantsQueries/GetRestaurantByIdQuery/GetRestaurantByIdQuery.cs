using MediatR;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.CQRS.Restaurants.RestaurantsQueries.GetRestaurantByIdQuery;

public class GetRestaurantByIdQuery(int id) : IRequest<RestaurantDto>
{
    public int  Id { get; set; } = id;
}