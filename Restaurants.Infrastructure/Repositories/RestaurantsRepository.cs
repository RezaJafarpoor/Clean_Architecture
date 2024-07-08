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

    public async Task<IEnumerable<Restaurant>> GetAllMatchingAsync(string? searchPhrase)
    {
        IEnumerable<Restaurant> restaurants;
        var searchLower = searchPhrase?.ToLower();
        if (searchLower is null)
        { restaurants = await dbContext.Restaurants.Include(d => d.Dishes).ToListAsync();
            return restaurants;
        }
        restaurants = await dbContext.Restaurants.Where(r => r.Name.ToLower().Contains(searchLower) || r.Description.ToLower().Contains(searchLower))
            .Include(r =>r.Dishes)
            .ToListAsync();
        return restaurants;


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