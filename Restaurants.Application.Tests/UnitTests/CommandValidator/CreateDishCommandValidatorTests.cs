using FluentValidation.TestHelper;
using Restaurants.Application.CQRS.Dishes.DishesCommands.CreateDishCommand;

namespace Restaurants.Application.Tests.UnitTests.CommandValidator;

public class CreateDishCommandValidatorTests
{

    [Fact]
    public void Validator_ShouldHaveNoError_WithValidCommand()
    {
        
        // Arrange
        var command = new CreateDishCommand
        {
            Price = 10,
            KiloCalories = 10
        
        };
        var validator = new CreateDishCommandValidator();
        // Act
        var result = validator.TestValidate(command);
        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
    [Fact]
    public void Validator_ShouldHaveError_WithInValidCommand()
    {
        
        // Arrange
        var command = new CreateDishCommand
        {
            Price = -1,
            KiloCalories = -10
        
        };
        var validator = new CreateDishCommandValidator();
        // Act
        var result = validator.TestValidate(command);
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Price)
            .WithErrorMessage("Price must be a non negative number");
        result.ShouldHaveValidationErrorFor(x => x.KiloCalories)
            .WithErrorMessage("KiloCalories must be a non negative number");
    }
}