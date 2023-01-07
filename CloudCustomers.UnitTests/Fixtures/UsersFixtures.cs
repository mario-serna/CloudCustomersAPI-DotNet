using CloudCustomers.API.Models;

namespace CloudCustomers.UnitTests.Fixtures;

public static class UsersFixture
{
    public static List<User> GetTestUsers() => new()
    {
        new User()
        {
            Id=Guid.NewGuid(),
            Name="User1",
            Email="user1@email.com",
            Address=new Address()
            {
                City="Test",
                State="Test",
                ZipCode=12345
            }
        },
        new User()
        {
            Id=Guid.NewGuid(),
            Name="User2",
            Email="user2@email.com",
            Address=new Address()
            {
                City="Test2",
                State="Test2",
                ZipCode=12345
            }
        }
    };
}