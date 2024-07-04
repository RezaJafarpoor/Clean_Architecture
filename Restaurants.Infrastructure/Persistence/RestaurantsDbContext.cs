using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence;

public class RestaurantsDbContext : DbContext
{
    
    
    
    internal DbSet<Restaurant> Restaurants { get; set; }
    internal DbSet<Dish> dishes { get; set; }

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Restaurant>()
            .OwnsOne(r => r.Address);
        
        modelBuilder.Entity<Restaurant>()
            .HasMany<Dish>(d => d.Dishes).WithOne()
            .HasForeignKey(f => f.RestaurantId);
    }

}