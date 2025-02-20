﻿using Restaurants.Application.Common;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
    Task<Restaurant?> GetByIdAsync(int id);
    Task<int> Create(Restaurant restaurant);
    Task DeleteAsync(Restaurant restaurant);
    Task SaveChanges();
}