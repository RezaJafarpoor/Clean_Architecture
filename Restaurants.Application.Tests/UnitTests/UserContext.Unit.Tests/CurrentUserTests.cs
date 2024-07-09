using FluentAssertions;
using Restaurants.Application.UserContext;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Tests.UnitTests.UserContext.Unit.Tests;

public class CurrentUserTests
{
    [Fact]
    public void IsInRole_ShouldReturnTrue_WithMatchingRole()
    {
        // Arrange
        var currentuser = new CurrentUser("1", "test@test.com",[UserRoles.User,UserRoles.Admin],null,null);


        // Act
       var result= currentuser.IsInRole(UserRoles.Admin);


        // Assert
        result.Should().BeTrue();
    }
    [Fact]
    public void IsInRole_ShouldTReturnFalse_WithWrongRole()
    {
        // Arrange
        var currentuser = new CurrentUser("1", "test@test.com",[UserRoles.User,UserRoles.Admin],null,null);


        // Act
        var result= currentuser.IsInRole(UserRoles.Owner);


        // Assert
        result.Should().BeFalse();
    }
}