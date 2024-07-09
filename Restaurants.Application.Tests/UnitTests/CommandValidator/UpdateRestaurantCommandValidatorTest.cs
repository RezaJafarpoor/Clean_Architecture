using FluentValidation.TestHelper;
using Restaurants.Application.CQRS.Restaurants.RestaurantsCommands.UpdateRestaurantCommand;

namespace Restaurants.Application.Tests.UnitTests.CommandValidator;

public class UpdateRestaurantCommandValidatorTest
{
    [Fact]
    public void Validator_shouldHaveNoErrors_withValidCommand()
    {
        // Arrange
        var command = new UpdateRestaurantCommand(1)
        {
            Name = "test",
            Description = "not null"
        };
        // Act

        var sut = new UpdateRestaurantCommandValidator();
        var result = sut.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
    [Fact]
    public void Validator_shouldHaveErrors_withInValidCommand()
    {
        // Arrange
        var command = new UpdateRestaurantCommand(1)
        {
            Name = "tt"
        };
        // Act

        var sut = new UpdateRestaurantCommandValidator();
        var result = sut.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x =>x.Description)
            .WithErrorMessage("Description is required");
        result.ShouldHaveValidationErrorFor(x => x.Name);

    }
}