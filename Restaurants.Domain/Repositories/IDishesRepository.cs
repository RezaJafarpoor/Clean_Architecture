using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IDishesRepository
{
    Task<int> CreateAsync(Dish dish);
    Task SaveChanges();
    Task<Dish> GetDishByIdAsync(int restaurantId ,int dishId);
    Task<bool> DeleteDishByIdAsync(int restaurantId, int dishId);
}