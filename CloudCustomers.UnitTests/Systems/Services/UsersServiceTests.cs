using CloudCustomers.API.Config;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace CloudCustomers.UnitTests.Systems.Services;

public class UsersServiceTests
{
    private readonly Mock<ILogger<UsersService>> loggerStub = new();

    [Fact]
    public async void GetAllUsers_WhenCalled_InvokeHttpGetRequest()
    {
        // Arrange
        string endpoint = "https://example.com/users";
        var expectedResponse = UsersFixture.GetTestUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        var usersService = new UsersService(loggerStub.Object, httpClient, config);
        // Act
        await usersService.GetAllUsers();
        // Assert
        handlerMock
        .Protected()
        .Verify(
            "SendAsync",
            Times.Exactly(1),
            ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
            ItExpr.IsAny<CancellationToken>()
        );
    }

    [Fact]
    public async void GetAllUsers_WhenCalled_ReturnsListOfUsers()
    {
        // Arrange
        string endpoint = "https://example.com/users";
        var expectedResponse = UsersFixture.GetTestUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        var usersService = new UsersService(loggerStub.Object, httpClient, config);
        // Act
        var result = await usersService.GetAllUsers();
        // Assert
        result.Should().BeOfType<List<User>>();
    }

    [Fact]
    public async void GetAllUsers_WhenHits404_ReturnsEmptyListOfUsers()
    {
        // Arrange
        string endpoint = "https://example.com/users";
        var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
        var httpClient = new HttpClient(handlerMock.Object);
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        var usersService = new UsersService(loggerStub.Object, httpClient, config);
        // Act
        var result = await usersService.GetAllUsers();
        // Assert
        result.Count.Should().Be(0);
    }

    [Fact]
    public async void GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
    {
        // Arrange
        string endpoint = "https://example.com/users";
        var expectedResponse = UsersFixture.GetTestUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        var usersService = new UsersService(loggerStub.Object, httpClient, config);
        // Act
        var result = await usersService.GetAllUsers();
        // Assert
        result.Count.Should().Be(expectedResponse.Count);
    }

    [Fact]
    public async void GetAllUsers_WhenCalled_InvokesConfiguredExternalUrl()
    {
        // Arrange
        var expectedResponse = UsersFixture.GetTestUsers();
        string endpoint = "https://example.com/users";
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse, endpoint);
        var httpClient = new HttpClient(handlerMock.Object);
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        var usersService = new UsersService(loggerStub.Object, httpClient, config);
        // Act
        var result = await usersService.GetAllUsers();
        // Assert
        handlerMock
        .Protected()
        .Verify(
            "SendAsync",
            Times.Exactly(1),
            ItExpr.Is<HttpRequestMessage>(
                req => req.Method == HttpMethod.Get
                       && req.RequestUri == new Uri(endpoint)
            ),
            ItExpr.IsAny<CancellationToken>()
        );
    }
}