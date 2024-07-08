using MediatR;
using Restaurants.Application.Common;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.CQRS.Restaurants.RestaurantsQueries.RestaurantGetAllQuery;

public class GetAllRestaurantsQuery(string? searchPhrase, int pageSize, int pageNumber) : IRequest<PagedResult<RestaurantDTO>>
{
    public string? SearchPhrase { get; } = searchPhrase;
    public int PageSize { get; } = pageSize;
    public int PageNumber { get; } = pageNumber;
}