using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.CQRS.DeleteRestaurantCommand;
using Restaurants.Application.CQRS.RestaurantCreateCommand;
using Restaurants.Application.CQRS.RestaurantsQueries.GetRestaurantByIdQuery;
using Restaurants.Application.CQRS.RestaurantsQueries.RestaurantGetAllQuery;
using Restaurants.Application.CQRS.UpdateRestaurantCommand;
using Restaurants.Application.DTOs;

namespace Restaurants.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RestaurantsController(IMediator mediator) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RestaurantDTO>>> GetRestaurants()
    {
        var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
        return Ok(restaurants);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RestaurantDTO>> GetRestaurantById(int id)
    {
        var restaurant =await mediator.Send(new GetRestaurantByIdQuery(id));
        if (restaurant is null)
        {
            return NotFound();
        }

        return Ok(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant(CreateRestaurantCommand command)
    {

        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetRestaurantById), new { id }, null);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteRestaurant(int id)
    {
        var isDeleted = await mediator.Send(new DeleteRestaurantCommand(id));
        if (isDeleted)
        {
            return NoContent();
        }

        return NotFound();
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateRestaurantById(int id, [FromBody]UpdateRestaurantCommand command)
    {
        command.Id = id;
        var isUpdated = await mediator.Send(command);
        if (isUpdated)
        {
            return NoContent();
        }

        return NotFound();
    }
}