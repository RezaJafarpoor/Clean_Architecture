namespace Restaurants.Application.UserContext;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}