using FluentValidation;

namespace Restaurants.Application.CQRS.CreateDishCommand;

public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
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