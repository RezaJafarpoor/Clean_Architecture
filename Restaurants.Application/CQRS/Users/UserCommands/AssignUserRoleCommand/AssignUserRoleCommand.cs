using MediatR;

namespace Restaurants.Application.CQRS.Users.UserCommands.AssignUserRoleCommand;

public class AssignUserRoleCommand : IRequest
{
    public string UserEmail { get; } = default!;
    public string RoleName { get; } = default!;
}