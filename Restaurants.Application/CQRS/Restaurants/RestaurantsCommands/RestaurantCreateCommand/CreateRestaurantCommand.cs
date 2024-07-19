using MediatR;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.CQRS.Restaurants.RestaurantsCommands.RestaurantCreateCommand;

public class CreateRestaurantCommand : IRequest<int>
{
    public int  Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public string? ContactEmail{ get; set; }
    
    public string? ContactNumber { get; set; }
    public bool HasDelivery { get; set; }
    
    public string City { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string PostalCode { get; set; } = default!;

   
}