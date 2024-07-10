namespace Restaurants.Domain.Repositories;

public interface ILoggerAdapter<T>
{
    void LogInformation(string? message, params object?[] args);
    void LogWarning(string? message, params object?[] args);
}