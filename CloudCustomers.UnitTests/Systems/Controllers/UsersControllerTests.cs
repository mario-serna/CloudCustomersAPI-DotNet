using CloudCustomers.API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace CloudCustomers.UnitTests.Systems.Controllers;

public class UsersControllerTests
{
    [Fact]
    public async Task Get_OnSuccess_ReturnsStatusCode200()
    {
        // Arrange
        var users = new UsersController();
        // Act
        var result = (OkObjectResult)await users.Get();
        // Assert
        result.StatusCode.Should().Be(200);
    }
}