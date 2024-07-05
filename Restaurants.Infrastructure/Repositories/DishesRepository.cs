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
}