using MediatR;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.CQRS.Restaurants.RestaurantsQueries.RestaurantGetAllQuery;

public class GetAllRestaurantsQuery(string? searchPhrase) : IRequest<IEnumerable<RestaurantDTO>>
{
    public string? SearchPhrase { get; } = searchPhrase;
}