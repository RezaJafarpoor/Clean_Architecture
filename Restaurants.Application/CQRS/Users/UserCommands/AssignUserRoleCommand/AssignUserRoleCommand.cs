using MediatR;

namespace Restaurants.Application.CQRS.Users.UserCommands.AssignUserRoleCommand;

public class AssignUserRoleCommand (string userEmail, string roleName): IRequest
{
    public string UserEmail { get; } = userEmail;
    public string RoleName { get; } = roleName;
}