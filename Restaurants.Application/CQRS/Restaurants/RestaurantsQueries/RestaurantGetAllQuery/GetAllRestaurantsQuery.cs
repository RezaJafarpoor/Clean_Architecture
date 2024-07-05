using MediatR;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.CQRS.Restaurants.RestaurantsQueries.RestaurantGetAllQuery;

public class GetAllRestaurantsQuery : IRequest<IEnumerable<RestaurantDTO>>
{
    
}