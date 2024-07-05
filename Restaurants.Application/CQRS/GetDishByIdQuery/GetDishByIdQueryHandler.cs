﻿using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.CQRS.GetDishByIdQuery;

public class GetDishByIdQueryHandler(ILogger<GetDishByIdQueryHandler> logger,
    IDishesRepository dishesRepository) : IRequestHandler<GetDishByIdQuery,DishDTO>
{
    public async Task<DishDTO> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
    {
        var dish = await dishesRepository.GetDishByIdAsync(request.RestaurantId, request.DishId);
        if (dish is null) throw new NotFoundException(nameof(Dish), request.DishId.ToString());
        var dishDto = DishDTO.FromEntity(dish);
        return dishDto!;
    }
}