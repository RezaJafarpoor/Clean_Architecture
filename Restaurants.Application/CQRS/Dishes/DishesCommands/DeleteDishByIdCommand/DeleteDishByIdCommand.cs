using MediatR;

namespace Restaurants.Application.CQRS.Dishes.DishesCommands.DeleteDishByIdCommand;

public class DeleteDishByIdCommand(int restaurantId, int dishId) : IRequest<bool>
{
    public int RestaurantId => restaurantId;

    public int DishId => dishId;
}