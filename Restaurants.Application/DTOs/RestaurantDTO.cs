﻿using Restaurants.Domain.Entities;

namespace Restaurants.Application.DTOs;

public class RestaurantDTO
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

    public static RestaurantDTO? FromEntity(Restaurant? restaurant)
    {
        if (restaurant == null) return null;
        return new RestaurantDTO()
        {
            Category = restaurant.Category,
            Description = restaurant.Description,
            Id = restaurant.Id,
            HasDelivery = restaurant.HasDelivery,
            Name = restaurant.Name,
            City = restaurant.Address?.City,
            Street = restaurant.Address.Street,
            PostalCode = restaurant.Address.PostalCode,
            Dishes = restaurant.Dishes.Select(DishDTO.FromEntity).ToList()
        };
    }

}