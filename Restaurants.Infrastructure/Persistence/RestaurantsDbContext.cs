﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence;

public class RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options) : IdentityDbContext<User>(options)
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
        modelBuilder.Entity<User>().HasMany(o => o.Restaurants)
            .WithOne(r => r.Owner)
            .HasForeignKey(r => r.OwnerId);
    }

}