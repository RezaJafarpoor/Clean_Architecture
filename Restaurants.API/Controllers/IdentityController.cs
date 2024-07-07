using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.CQRS.Users.UserCommands.AssignUserRoleCommand;
using Restaurants.Application.CQRS.Users.UserCommands.UpdateDetailsCommand;
using Restaurants.Domain.Constants;

namespace Restaurants.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class IdentityController(IMediator mediator) : ControllerBase
{

    [HttpPatch("updateUser")]
    [Authorize]
    public async Task<IActionResult> UpdateUserDetails(UpdateDetailsCommand command)
    {
        await mediator.Send(command);
        return NoContent();

    }

    [HttpPost("assignRole")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
    [HttpDelete("unassignRole")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> UnassignUserRole(UnassignUserRoleCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
}

public class UnassignUserRoleCommand
{
}