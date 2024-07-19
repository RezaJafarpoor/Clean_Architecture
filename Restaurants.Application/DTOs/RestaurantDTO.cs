using Restaurants.Domain.Entities;

namespace Restaurants.Application.DTOs;

public class RestaurantDto
{
    public int  Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    
    public bool HasDelivery { get; set; }
    
    public string City { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string PostalCode { get; set; } = default!;
    public List<DishDTO?> Dishes { get; set; } = [];

   

}