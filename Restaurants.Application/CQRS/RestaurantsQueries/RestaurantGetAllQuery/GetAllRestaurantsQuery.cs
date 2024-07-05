using MediatR;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.CQRS.RestaurantsQueries.RestaurantGetAllQuery;

public class GetAllRestaurantsQuery : IRequest<IEnumerable<RestaurantDTO>>
{
    
}