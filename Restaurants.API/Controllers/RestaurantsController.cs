using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.DTOs;
using Restaurants.Application.Services;
using Restaurants.Domain.Entities;

namespace Restaurants.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RestaurantsController(IRestaurantService restaurantService) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RestaurantDTO>>> GetRestaurants()
    {
        var restaurants = await restaurantService.GetAllRestaurants();
        return Ok(restaurants);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RestaurantDTO>> GetRestaurantById(int id)
    {
        var restaurant =await restaurantService.GetRestaurantById(id);
        if (restaurant is null)
        {
            return NotFound();
        }

        return Ok(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant(CreateRestaurantDTO restaurantDto)
    {
        
        var id = await restaurantService.CreateRestaurant(restaurantDto);
        return CreatedAtAction(nameof(GetRestaurantById), new { id }, null);
    }
}