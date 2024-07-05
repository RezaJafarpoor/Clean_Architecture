﻿using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.CQRS.Dishes.DishesCommands.DeleteDishByIdCommand;

public class DeleteDishByIdCommandHandler(ILogger<DeleteDishByIdCommandHandler> logger,
    IDishesRepository dishesRepository) : IRequestHandler<DeleteDishByIdCommand, bool>
{
    public async Task<bool> Handle(DeleteDishByIdCommand request, CancellationToken cancellationToken)
    {
        var result = await dishesRepository.DeleteDishByIdAsync(request.RestaurantId, request.DishId);
        return false;
    }
}