﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.CQRS.DeleteRestaurantCommand;
using Restaurants.Application.CQRS.RestaurantCreateCommand;
using Restaurants.Application.CQRS.RestaurantsQueries.GetRestaurantByIdQuery;
using Restaurants.Application.CQRS.RestaurantsQueries.RestaurantGetAllQuery;
using Restaurants.Application.CQRS.UpdateRestaurantCommand;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Exceptions;
using System.Runtime.CompilerServices;

namespace Restaurants.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RestaurantsController(IMediator mediator) : ControllerBase
{

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RestaurantDTO>>> GetRestaurants()
    {
        var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
        return Ok(restaurants);
    }

    [HttpGet("{id:int}")]
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
        
        return NotFound();
    }

    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateRestaurantById(int id, [FromBody]UpdateRestaurantCommand command)
    {
        command.Id = id;
        await mediator.Send(command);
        return NotFound();
        

    }
}