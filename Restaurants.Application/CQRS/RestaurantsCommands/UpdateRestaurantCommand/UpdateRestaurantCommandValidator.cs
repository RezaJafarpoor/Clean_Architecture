using FluentValidation;

namespace Restaurants.Application.CQRS.RestaurantsCommands.UpdateRestaurantCommand;

public class UpdateRestaurantCommandValidator : AbstractValidator<RestaurantsCommands.UpdateRestaurantCommand.UpdateRestaurantCommand>
{
    public UpdateRestaurantCommandValidator()
    {
        RuleFor(x => x.Name)
            .Length(3, 100);
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        
    }
}