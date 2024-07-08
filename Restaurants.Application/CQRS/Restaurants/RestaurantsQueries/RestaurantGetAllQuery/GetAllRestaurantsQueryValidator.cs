using FluentValidation;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.CQRS.Restaurants.RestaurantsQueries.RestaurantGetAllQuery;


public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private int[] _allowPageSizes = [5, 10, 15, 30];

    private string[] _allowSortByColumns =
        [nameof(Restaurant.Category), nameof(Restaurant.Name), nameof(Restaurant.Description)];
    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);
        RuleFor(r => r.PageSize)
            .Must(value => _allowPageSizes.Contains(value))
            .WithMessage($"page size must be in [ {string.Join(",", _allowPageSizes)} ]");
        RuleFor(r => r.SortBy)
            .Must(value => _allowSortByColumns.Contains(value))
            .When(q => q.SortBy!=null)
            .WithMessage($"page size must be in [ {string.Join(",", _allowSortByColumns)} ]");
    }
    
}