﻿using System.Diagnostics;

namespace Restaurants.API.Middlewares;

public class RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var stopwatch = Stopwatch.StartNew();
        await next.Invoke(context);
        stopwatch.Stop();
        if (stopwatch.ElapsedMilliseconds > 4000)
        {
            logger.LogInformation("Request [{Verb}] at {path} took {time} ms",context.Request.Method,
                context.Request.Path,
                stopwatch.ElapsedMilliseconds);
        }
    }
}