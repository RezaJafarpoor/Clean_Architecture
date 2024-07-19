using MediatR;
using Restaurants.Application.Common;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.CQRS.Restaurants.RestaurantsQueries.RestaurantGetAllQuery;

public class GetAllRestaurantsQuery(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection
) : IRequest<PagedResult<RestaurantDto>>
{
    public string? SearchPhrase { get; } = searchPhrase;
    public int PageSize { get; } = pageSize;
    public int PageNumber { get; } = pageNumber;
    public string? SortBy { get; } = sortBy;
    public SortDirection SortDirection { get; } = sortDirection;
}