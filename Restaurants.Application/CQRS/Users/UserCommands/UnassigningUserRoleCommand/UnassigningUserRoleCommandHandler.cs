using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.CQRS.Users.UserCommands.UnassigningUserRoleCommand;

public class UnassigningUserRoleCommandHandler(ILogger<UnassigningUserRoleCommand> logger,
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager) : IRequestHandler<UnassigningUserRoleCommand>
{
    public async Task Handle(UnassigningUserRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Remove role {@role} from user{user}", request.RoleName, request.UserEmail);
       var user = await userManager.FindByEmailAsync(request.UserEmail)
            ?? throw new NotFoundException(nameof(User), request.UserEmail);
       var role = await roleManager.FindByNameAsync(request.RoleName)
                  ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName);
       await userManager.RemoveFromRoleAsync(user, role.Name);
    }
}