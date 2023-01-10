using CloudCustomers.API.Config;
using CloudCustomers.API.Models;
using Microsoft.Extensions.Options;

namespace CloudCustomers.API.Services;

public interface IUsersService
{
    Task<List<User>> GetAllUsers();
}

public class UsersService : IUsersService
{
    private readonly ILogger<UsersService> _logger;
    private readonly HttpClient _httpClient;
    private readonly UsersApiOptions _apiConfig;

    public UsersService(
        ILogger<UsersService> logger,
        HttpClient httpClient,
        IOptions<UsersApiOptions> apiConfig
    )
    {
        _logger = logger;
        _httpClient = httpClient;
        _apiConfig = apiConfig.Value;
    }

    public async Task<List<User>> GetAllUsers()
    {
        try
        {
            var response = await _httpClient.GetAsync(_apiConfig.Endpoint);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new List<User>();
            }

            var responseContent = response.Content;
            var allUsers = await responseContent.ReadFromJsonAsync<List<User>>();
            return allUsers.ToList();
        }
        catch (System.Exception exception)
        {
            _logger.LogError(exception.Message);
            return null;
        }
    }
}