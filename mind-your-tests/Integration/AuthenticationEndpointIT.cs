using mind_your_domain;
using mind_your_money_server.Api.DTOs;
using mind_your_money_server.Api.Endpoints;
using mind_your_tests.Generators;
using mind_your_tests.Utilities;

namespace mind_your_tests.Integration;

public class AuthenticationEndpointIt : DatabaseIntegrationTest<User>
{
    [Test]
    public async Task LogIn_CorrectCredentials()
    {
        // Arrange
        var user = UserGenerator.Generate(1)[0];
        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        var request = new LogInRequest();
        request.Password = user.Password;
        request.Username = user.Name;

        var result = AuthenticationEndpoint.LogIn(request, _db);
        
        
        
    }

}