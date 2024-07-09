using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using Restaurants.Application.UserContext;
using Restaurants.Domain.Constants;
using System.Security.Claims;

namespace Restaurants.Application.Tests.UnitTests.UserContext.Unit.Tests;

public class UserContextTests
{
    private readonly IHttpContextAccessor _httpContextAccessor = Substitute.For<IHttpContextAccessor>();
    private readonly IUserContext _sut;

    public UserContextTests()
    {
        _sut = new Application.UserContext.UserContext(_httpContextAccessor);
    }

    [Fact]
    public void GetCurrentUser_ShouldReturnCurrentUser_WhenUserContextExistAndUserAuthenticated
        ()
    {
        // Arrange
        var dateOfBirth = new DateOnly(1990, 1, 1);
        var id = "1";
        var email = "test@test.com";
        var userRoles = new List<string>
        {
            UserRoles.Admin,
            UserRoles.User
        };
        var nationality = "iran";
        var claims = new List<Claim>
        {
            
            new (ClaimTypes.NameIdentifier,id),
            new (ClaimTypes.Email,email),
            new (ClaimTypes.Role,userRoles[0]),
            new (ClaimTypes.Role,userRoles[1]),
            new ("Nationality",nationality),
            new ("DateOfBirth",dateOfBirth.ToString("yyyy MM dd"))
        };
        
        var date = dateOfBirth.ToString("yyyy MM dd");
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "test"));
        
        _httpContextAccessor.HttpContext?.User.Returns(user);
        
        // Act
        var result = _sut.GetCurrentUser();
        
        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(id);
        result.Email.Should().Be(email);
        result.Nationality.Should().Be(nationality);
        result.Roles.Should().Contain(userRoles);
        result.DateOfBirth.Should().Be(dateOfBirth);
    }
    [Fact]
    public void GetCurrentUser_ShouldReturnCurrentUser_WhenUserContextExistButUserIsNotAuthenticated
        ()
    {
        // Arrange
        var dateOfBirth = new DateOnly(1990, 1, 1);
        var id = "1";
        var email = "test@test.com";
        var userRoles = new List<string>
        {
            UserRoles.Admin,
            UserRoles.User
        };
        var nationality = "iran";
        var claims = new List<Claim>
        {
            
            new (ClaimTypes.NameIdentifier,id),
            new (ClaimTypes.Email,email),
            new (ClaimTypes.Role,userRoles[0]),
            new (ClaimTypes.Role,userRoles[1]),
            new ("Nationality",nationality),
            new ("DateOfBirth",dateOfBirth.ToString("yyyy MM dd"))
        };
        
        var date = dateOfBirth.ToString("yyyy MM dd");
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims));
        
        _httpContextAccessor.HttpContext?.User.Returns(user);
        
        // Act
        var result = _sut.GetCurrentUser();
        
        // Assert
        result.Should().BeNull();
    }
    [Fact]
    public void GetCurrentUser_ShouldReturnCurrentUser_WhenUserContextIsNotExist()
    {
        // Arrange
        var exception = new InvalidOperationException("User Context is not present");
        _httpContextAccessor.HttpContext?.User.Throws(exception);
        
        // Act
        var requestAction = ()=> _sut.GetCurrentUser();
        
        // Assert

        requestAction.Should().Throw<InvalidOperationException>().WithMessage("User Context is not present");

    }
}