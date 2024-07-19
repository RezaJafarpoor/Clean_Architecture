using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.CQRS.Restaurants.RestaurantsQueries.RestaurantGetAllQuery;

public class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger,
    IRestaurantRepository repository) : IRequestHandler<GetAllRestaurantsQuery, PagedResult<RestaurantDto>>
{
    public async Task<PagedResult<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Getting All Restaurants");
        var (restaurants, totalCount) = await repository.GetAllMatchingAsync(request.SearchPhrase,request.PageSize,request.PageNumber,request.SortBy, request.SortDirection);
        var restaurantsDto = restaurants.Select(request.ToRestaurantDto);
        var result = new PagedResult<RestaurantDto>(restaurantsDto, totalCount , request.PageSize, request.PageNumber);
        return result;
    }
}