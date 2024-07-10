using FluentValidation.TestHelper;
using Restaurants.Application.Common;
using Restaurants.Application.CQRS.Restaurants.RestaurantsQueries.RestaurantGetAllQuery;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Tests.UnitTests.CommandValidator;

public class GetAllRestaurantsQueryValidatorTests
{
    [Fact]
    public void Validator_ShouldHaveNoErrors_WithValidCommand()
    {
     
        // Arrange
        
        var command = new GetAllRestaurantsQuery(
            null, 15, 4, null, SortDirection.Ascending);
        
        // Act
        
        var sut = new GetAllRestaurantsQueryValidator();
        var result = sut.TestValidate(command);
        
        // Assert
        result.ShouldNotHaveAnyValidationErrors();
        
    }
    [Fact]
    public void Validator_ShouldHaveErrors_WithInValidCommand()
    {
     
        // Arrange
         int[] _allowPageSizes = [5, 10, 15, 30];
         string[] _allowSortByColumns =
             [nameof(Restaurant.Category), nameof(Restaurant.Name), nameof(Restaurant.Description)];

        var command = new GetAllRestaurantsQuery(
            null, 100, -1, nameof(Restaurant.HasDelivery), SortDirection.Ascending);
        
        // Act
        
        var sut = new GetAllRestaurantsQueryValidator();
        var result = sut.TestValidate(command);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x =>x.PageNumber);
        result.ShouldHaveValidationErrorFor(x =>x.PageSize)
            .WithErrorMessage($"page size must be in [ {string.Join(",", _allowPageSizes)} ]");
        result.ShouldHaveValidationErrorFor(x => x.SortBy)
            .WithErrorMessage($"page size must be in [ {string.Join(",", _allowSortByColumns)} ]");


    }
}