using MediatR;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.CQRS.RestaurantsQueries.GetRestaurantByIdQuery;

public class GetRestaurantByIdQuery : IRequest<RestaurantDTO?>
{
    public GetRestaurantByIdQuery(int id)
    {
        Id = id;
    }
    public int  Id { get; set; }
}