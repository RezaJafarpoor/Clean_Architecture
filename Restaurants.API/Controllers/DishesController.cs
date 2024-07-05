using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.CQRS.CreateDishCommand;
using Restaurants.Application.CQRS.GetDishByIdQuery;
using Restaurants.Application.CQRS.GetDishesForRestaurantQuery;
using Restaurants.Application.DTOs;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurants/{restaurantId:int}/[controller]")]
public class DishesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateDish([FromRoute] int restaurantId,CreateDishCommand command)
    {
        command.RestaurantId = restaurantId;
        await mediator.Send(command);
        return Created();
    }

    [HttpGet]
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
}