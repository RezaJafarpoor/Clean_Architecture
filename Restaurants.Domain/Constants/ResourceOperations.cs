using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Restaurants.Domain.Constants;

public enum ResourceOperations
{
    Create,
    Read,
    Update,
    Delete,
}