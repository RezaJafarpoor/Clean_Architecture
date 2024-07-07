using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.UserContext;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.CQRS.Users.UserCommands.UpdateDetailsCommand;

public class UpdateDetailsCommandHandler(ILogger<UpdateDetailsCommandHandler> logger,
    IUserContext userContext,
    IUserStore<User> userStore) : IRequestHandler<UpdateDetailsCommand>
{
    public async Task Handle(UserCommands.UpdateDetailsCommand.UpdateDetailsCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        logger.LogInformation("Updating user : {UserId} with {@Request}",currentUser!.Id, request);
        var user = await userStore.FindByIdAsync(currentUser.Id, cancellationToken);
        if (user == null) throw new NotFoundException(nameof(User), user!.Id);
        user.Nationality = request.Nationality;
        user.DateOfBirth = request.DateOfBirth;
        await userStore.UpdateAsync(user, cancellationToken);
    }
}