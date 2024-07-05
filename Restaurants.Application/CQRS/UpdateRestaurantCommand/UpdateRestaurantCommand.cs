using MediatR;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.CQRS.UpdateRestaurantCommand;

public class UpdateRestaurantCommand(int id) : IRequest
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public bool HasDelivery { get; set; }

    internal void FromEntity(UpdateRestaurantCommand source,Restaurant destination)
    {

        destination.Name = source.Name;
        destination.Description = source.Description;
        destination.HasDelivery = source.HasDelivery;

    }
}