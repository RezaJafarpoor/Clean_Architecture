using FluentValidation;

namespace Restaurants.Application.CQRS.Dishes.DishesCommands.CreateDishCommand;

public class CreateDishCommandValidator : AbstractValidator<Dishes.DishesCommands.CreateDishCommand.CreateDishCommand>
{
    public CreateDishCommandValidator()
    {
        RuleFor(d => d.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price must be a non negative number");
        RuleFor(d => d.KiloCalories)
            .GreaterThanOrEqualTo(0)
            .WithMessage("KiloCalories must be a non negative number");
    }
}