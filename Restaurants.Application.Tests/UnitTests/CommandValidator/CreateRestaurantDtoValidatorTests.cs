using FluentAssertions;
using FluentValidation.TestHelper;
using Restaurants.Application.CQRS.Restaurants.RestaurantsCommands.RestaurantCreateCommand;

namespace Restaurants.Application.Tests.UnitTests.CommandValidator;

public class CreateRestaurantDtoValidatorTests
{
    private readonly CreateRestaurantDtoValidator _sut;

    public CreateRestaurantDtoValidatorTests()
    {
        _sut = new CreateRestaurantDtoValidator();
    }

    [Fact]
    public void Validator_ShouldNotHaveErrors_WhenCommandIsValid()
    {
        // Arrange
        
        var command = new CreateRestaurantCommand
        {
            Name = "test",
            Category = "Italian",
            ContactEmail = "test@test.com",
            PostalCode = "12-345"
        };
        
        // Act

        var result =_sut.TestValidate(command);
        
        // Assert

        result.ShouldHaveAnyValidationError();
    }
    [Fact]
    public void Validator_ShouldHaveErrors_WhenCommandIsInValid()
    {
        // Arrange
        
        var command = new CreateRestaurantCommand
        {
            Name = "te",
            Category = "Iran",
            ContactEmail = "@test.com",
            PostalCode = "1-345"
        };
        
        // Act

        var result =_sut.TestValidate(command);
        
        // Assert

        result.ShouldHaveValidationErrorFor(x => x.Name);
        result.ShouldHaveValidationErrorFor(x => x.Category);
        result.ShouldHaveValidationErrorFor(x => x.ContactEmail);
        result.ShouldHaveValidationErrorFor(x => x.PostalCode);

    }
}