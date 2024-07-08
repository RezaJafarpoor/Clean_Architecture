using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

public class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantRepository
{
    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await dbContext.Restaurants.Include(d => d.Dishes).ToListAsync();
        return restaurants;
    }

    public async Task<(IEnumerable<Restaurant>,int)> GetAllMatchingAsync(string? searchPhrase, int pageSize,int pageNumber)
    {
        var skip = pageSize * (pageNumber - 1);
        var take =  pageSize;
        var searchLower = searchPhrase?.ToLower();
        var baseQuery = dbContext.Restaurants.
            Where(r => searchLower == null || (r.Name.ToLower().Contains(searchLower)
                                               || r.Description.ToLower()
                                                   .Contains(searchLower)));
        var totalCount = await baseQuery.CountAsync();
        var restaurant = await baseQuery.Skip(skip).Take(take).ToListAsync();

        return (restaurant, totalCount);
    }

    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        var restaurant = await dbContext.Restaurants.
            Include(d =>d.Dishes).FirstOrDefaultAsync(x => x.Id == id);
        return restaurant;
    }

    public async Task<int> Create(Restaurant restaurant)
    {
        dbContext.Restaurants.Add(restaurant);
        await SaveChanges();
        return restaurant.Id;
    }

    public async Task DeleteAsync(Restaurant restaurant)
    {   
        dbContext.Remove(restaurant);
        await SaveChanges();

    }

    public async Task SaveChanges() => await dbContext.SaveChangesAsync();
    
}