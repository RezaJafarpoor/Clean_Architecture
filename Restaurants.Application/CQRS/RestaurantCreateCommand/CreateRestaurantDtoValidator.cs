using FluentValidation;

namespace Restaurants.Application.CQRS.RestaurantCreateCommand;

public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string> validCategories = ["Italian", "Mexican", "Japanese", "American", "Indian"];
    public CreateRestaurantDtoValidator()
    {
        RuleFor(x => x.Category).Must(category => validCategories.Contains(category)).WithMessage("Category is not valid");
        RuleFor(x => x.Name)
            .Length(3, 100);
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.ContactEmail).EmailAddress().WithMessage("Please Provide valid Email Address");
        RuleFor(x => x.PostalCode).Matches(@"^\d{2}-\d{3]$").WithMessage("Please Provide a valid postal code (XX-XXX)");
    }
}