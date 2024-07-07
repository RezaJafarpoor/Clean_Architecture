using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.CQRS.Dishes.DishesCommands.CreateDishCommand;
using Restaurants.Application.CQRS.Dishes.DishesCommands.DeleteDishByIdCommand;
using Restaurants.Application.CQRS.Dishes.DishesQueries.GetDishByIdQuery;
using Restaurants.Application.CQRS.Dishes.DishesQueries.GetDishesForRestaurantQuery;
using Restaurants.Application.DTOs;
using Restaurants.Infrastructure.Constants;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurants/{restaurantId:int}/[controller]")]
[Authorize]
public class DishesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateDish([FromRoute] int restaurantId,CreateDishCommand command)
    {
        command.RestaurantId = restaurantId;
        var dishId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetDishByIdForRestaurant), new { restaurantId, dishId }, null);
    }

    [HttpGet]
    [Authorize(Policy = PolicyNames.MinimumAge)]
    public async Task<ActionResult<IEnumerable<DishDTO>>> GetAllDishesForRestaurant([FromRoute] int restaurantId)
    {
        var dishes = await mediator.Send(new GetDishesForRestaurantQuery(restaurantId));
        return Ok(dishes);
    }

    [HttpGet("{dishId:int}")]
    public async Task<ActionResult<DishDTO>> GetDishByIdForRestaurant([FromRoute] int restaurantId, int dishId)
    {
        var dish = await mediator.Send(new  GetDishByIdQuery(restaurantId, dishId));
        return Ok(dish);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteDishById([FromRoute] int restaurantId, int id)
    {
        var result = await mediator.Send(new DeleteDishByIdCommand(restaurantId, id));
        if (result) return NoContent();
        return NotFound();

    }
}