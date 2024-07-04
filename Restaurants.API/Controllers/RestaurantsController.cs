using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Services;
using Restaurants.Domain.Entities;

namespace Restaurants.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RestaurantsController(IRestaurantService restaurantService) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
    {
        var restaurants = await restaurantService.GetAllRestaurants();
        return Ok(restaurants);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Restaurant>> GetRestaurantById(int id)
    {
        var restaurant =await restaurantService.GetRestaurantById(id);
        if (restaurant is null)
        {
            return NotFound();
        }

        return Ok(restaurant);
    }
}