using FluentAssertions;
using Restaurants.Application.UserContext;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Tests.UnitTests.UserContext.Unit.Tests;

public class CurrentUserTests
{
    [Theory]
    [InlineData("1", "test@test.com",UserRoles.User,UserRoles.Admin,null,null)]
    [InlineData("1", "test@test.com",UserRoles.User,UserRoles.Owner,null,null)]

    public void IsInRole_ShouldReturnTrue_WithMatchingRoles(string id, string email,string? first, string? second, string? nationality
    ,DateOnly? dateofbirth)
    {
        // Arrange
        var currentuser = new CurrentUser(id, email,[first,second],nationality,dateofbirth);


        // Act
       var firstRole= currentuser.IsInRole(first);
       var secondRole= currentuser.IsInRole(second);



        // Assert
        firstRole.Should().BeTrue();
        secondRole.Should().BeTrue();
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