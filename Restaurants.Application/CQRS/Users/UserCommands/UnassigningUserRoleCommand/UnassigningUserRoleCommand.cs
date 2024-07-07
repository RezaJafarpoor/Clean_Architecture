using MediatR;

namespace Restaurants.Application.CQRS.Users.UserCommands.UnassigningUserRoleCommand;

public class UnassigningUserRoleCommand : IRequest
{
    public string RoleName { get; } = default!;
    public string UserEmail { get; } = default!;
}