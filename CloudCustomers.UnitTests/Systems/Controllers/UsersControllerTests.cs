using CloudCustomers.API.Controllers;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CloudCustomers.UnitTests.Systems.Controllers;

public class UsersControllerTests
{
    private readonly Mock<IUsersService> mockUsersService = new();
    [Fact]
    public async Task Get_OnSuccess_ReturnsStatusCode200()
    {
        // Arrange
        mockUsersService.Setup(service => service.GetAllUsers())
                       .ReturnsAsync(UsersFixture.GetTestUsers());

        var users = new UsersController(mockUsersService.Object);
        // Act
        var result = (OkObjectResult)await users.Get();
        // Assert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_OnSuccess_InvokesUsersServiceExactlyOnce()
    {
        // Arrange
        mockUsersService.Setup(service => service.GetAllUsers())
                       .ReturnsAsync(new List<User>());

        var usersController = new UsersController(mockUsersService.Object);
        // Act
        var result = await usersController.Get();
        // Assert
        mockUsersService.Verify(
            service => service.GetAllUsers(),
            Times.Once()
        );
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnsListOfUsers()
    {
        // Arrange
        mockUsersService.Setup(service => service.GetAllUsers())
                       .ReturnsAsync(UsersFixture.GetTestUsers());

        var usersController = new UsersController(mockUsersService.Object);
        // Act
        var result = await usersController.Get();
        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var objectResult = (OkObjectResult)result;
        objectResult.Value.Should().BeOfType<List<User>>();
    }

    [Fact]
    public async Task Get_OnNoUsersFound_ReturnsStatusCode404()
    {
        // Arrange
        mockUsersService.Setup(service => service.GetAllUsers())
                       .ReturnsAsync(new List<User>());

        var usersController = new UsersController(mockUsersService.Object);
        // Act
        var result = await usersController.Get();
        // Assert
        result.Should().BeOfType<NotFoundResult>();
        var objectResult = (NotFoundResult)result;
        objectResult.StatusCode.Should().Be(404);

    }
}