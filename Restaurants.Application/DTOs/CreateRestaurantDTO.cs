using Restaurants.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Restaurants.Application.DTOs;

public class CreateRestaurantDTO
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    
    public string Category { get; set; } = default!;
    
    public bool HasDelivery { get; set; }
    public string? ContactEmail{ get; set; }
    
    public string? ContactNumber { get; set; }
    
    public string City { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string PostalCode { get; set; } = default!;

    public static Restaurant FromEntity(CreateRestaurantDTO restaurantDto)
    {

        return new Restaurant()
        {
            Name = restaurantDto.Name,
            Description = restaurantDto.Description,
            Category = restaurantDto.Category,
            HasDelivery = restaurantDto.HasDelivery,
            ContactEmail = restaurantDto.ContactEmail,
            ContactNumber = restaurantDto.ContactNumber,
            Address = new Address()
            {
                City = restaurantDto.City,
                Street = restaurantDto.Street,
                PostalCode = restaurantDto.PostalCode
            }
        };
    }
}