using Microsoft.AspNetCore.Mvc;
using Restaurants.Domain.Entities;

namespace Restaurants.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{

    [HttpGet]
    public async Task<List<Restaurant>> GetRestaurants()
    {
        throw new NotImplementedException();
    }
}