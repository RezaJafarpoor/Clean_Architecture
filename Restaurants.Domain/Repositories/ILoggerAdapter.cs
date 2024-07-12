namespace Restaurants.Application.Common;

public interface ILoggerAdapter<T>
{
    void LogInformation(string? message, params object?[] args);
    void LogWarning(string? message, params object?[] args);
}