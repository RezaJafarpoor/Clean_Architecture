using Microsoft.EntityFrameworkCore;
using Restaurants.Application.Common;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace Restaurants.Infrastructure.Repositories;

public class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantRepository
{
    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await dbContext.Restaurants.Include(d => d.Dishes).ToListAsync();
        return restaurants;
    }

    public async Task<(IEnumerable<Restaurant>,int)> GetAllMatchingAsync(string? searchPhrase, int pageSize,int pageNumber, string? sortBy, SortDirection sortDirection)
    {
        var skip = pageSize * (pageNumber - 1);
        var take =  pageSize;
        var searchLower = searchPhrase?.ToLower();
        var baseQuery = dbContext.Restaurants.
            Where(r => searchLower == null || (r.Name.ToLower().Contains(searchLower)
                                               || r.Description.ToLower()
                                                   .Contains(searchLower)));
        var totalCount = await baseQuery.CountAsync();
        if (sortBy != null)
        {
            var columnSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
            {
                {nameof(Restaurant.Name), r => r.Name},
                {nameof(Restaurant.Description), r => r.Description},
                {nameof(Restaurant.Category), r => r.Category}
            };
            var selectedColumn = columnSelector[sortBy];
            baseQuery =  sortDirection == SortDirection.Ascending
            ? baseQuery.OrderBy(selectedColumn)
            : baseQuery.OrderByDescending(selectedColumn);
        }
        
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