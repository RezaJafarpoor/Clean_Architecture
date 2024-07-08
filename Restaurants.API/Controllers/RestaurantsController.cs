using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.CQRS.Restaurants.RestaurantsCommands.DeleteRestaurantCommand;
using Restaurants.Application.CQRS.Restaurants.RestaurantsCommands.RestaurantCreateCommand;
using Restaurants.Application.CQRS.Restaurants.RestaurantsCommands.UpdateRestaurantCommand;
using Restaurants.Application.CQRS.Restaurants.RestaurantsQueries.GetRestaurantByIdQuery;
using Restaurants.Application.CQRS.Restaurants.RestaurantsQueries.RestaurantGetAllQuery;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Constants;
using Restaurants.Infrastructure.Constants;

namespace Restaurants.API.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RestaurantsController(IMediator mediator) : ControllerBase
{

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<RestaurantDTO>>> GetRestaurants([FromQuery] string? searchPhrase, int pageSize, int pageNumber)
    {
        var restaurants = await mediator.Send(new GetAllRestaurantsQuery(searchPhrase, pageSize, pageNumber));
        return Ok(restaurants);
    }

    [HttpGet("{id:int}")]
    [Authorize(Policy = PolicyNames.HasNationality)]
    public async Task<ActionResult<RestaurantDTO>> GetRestaurantById(int id)
    {
        
        var restaurant =await mediator.Send(new GetRestaurantByIdQuery(id));
        
        return Ok(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant(CreateRestaurantCommand command)
    {
        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetRestaurantById), new { id }, null);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteRestaurant(int id)
    {
        await mediator.Send(new DeleteRestaurantCommand(id));
        
        return NoContent();
    }

    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize(Roles = UserRoles.Owner)]
    public async Task<IActionResult> UpdateRestaurantById(int id, [FromBody]UpdateRestaurantCommand command)
    {
        command.Id = id;
        await mediator.Send(command);
        return NoContent();
        

    }
}