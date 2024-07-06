using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.CQRS.Users.UserCommands;

namespace Restaurants.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class IdentityController(IMediator mediator) : ControllerBase
{

    [HttpPatch]
    [Authorize]
    public async Task<IActionResult> UpdateUserDetails(UpdateDetailsCommand command)
    {
        await mediator.Send(command);
        return NoContent();

    }
}