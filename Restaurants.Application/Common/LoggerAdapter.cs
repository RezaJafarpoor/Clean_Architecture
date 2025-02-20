﻿using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Common;

public class LoggerAdapter<T> : ILoggerAdapter<T>
{
    private readonly ILogger<T> _logger;

    public LoggerAdapter(ILogger<T> logger)
    {
        _logger = logger;
    }
    public void LogInformation(string? message, params object?[] args)
    {
        _logger.LogInformation(message, args);
    }

    public void LogWarning(string? message, params object?[] args)
    {
        _logger.LogWarning(message, args);

    }
}