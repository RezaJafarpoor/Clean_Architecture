using FluentValidation;

namespace Restaurants.Application.CQRS.RestaurantsCommands.RestaurantCreateCommand;

public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string> _validCategories = ["Italian", "Mexican", "Japanese", "American", "Indian"];
    private const string Pattern = @"^\d{2}-\d{3}$";
    public CreateRestaurantDtoValidator()
    {
        RuleFor(x => x.Category).Must(category => _validCategories.Contains(category)).WithMessage("Category is not valid");
        RuleFor(x => x.Name)
            .Length(3, 100);
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.ContactEmail).EmailAddress().WithMessage("Please Provide valid Email Address");
        RuleFor(x => x.PostalCode).Matches(Pattern).WithMessage("Please Provide a valid postal code (XX-XXX)");
    }
}