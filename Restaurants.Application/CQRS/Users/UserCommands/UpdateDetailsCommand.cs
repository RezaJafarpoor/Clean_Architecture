using MediatR;

namespace Restaurants.Application.CQRS.Users.UserCommands;

public class UpdateDetailsCommand : IRequest
{

    public DateOnly? DateOfBirth { get; set; }
    public string? Nationality { get; set; }
    
}