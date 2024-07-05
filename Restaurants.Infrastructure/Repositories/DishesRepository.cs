using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

public class DishesRepository(RestaurantsDbContext dbContext) : IDishesRepository
{
    public async Task<int> CreateAsync(Dish dish)
    {
         dbContext.dishes.Add(dish);
         await SaveChanges();
         return dish.Id;
    }

    public async Task SaveChanges()
    {
        await dbContext.SaveChangesAsync();
    }

    public async Task<Dish> GetDishByIdAsync(int restaurantId,int dishId)
    {
        var dish = await dbContext.dishes.Where(x => x.RestaurantId == restaurantId && x.Id == dishId).FirstOrDefaultAsync();
        return dish!;
    }

    public async Task<bool> DeleteDishByIdAsync(int restaurantId, int dishId)
    {
        IQueryable queryable = dbContext.dishes.Where(r => r.RestaurantId == restaurantId && r.Id == dishId);
         dbContext.Remove(queryable);
         var result = await dbContext.SaveChangesAsync() > 0;
         return result;
    }
}