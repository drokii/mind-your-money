using Microsoft.AspNetCore.Http.HttpResults;
using mind_your_domain;
using mind_your_money_server.Api.DTOs;
using mind_your_money_server.Api.Endpoints;
using mind_your_money_server.Database.Services;
using mind_your_tests.Utilities;

namespace mind_your_tests.Integration;

public class AuthenticationEndpointIt : DatabaseIntegrationTest<User>
{
    private UserService _service = null!;

    [OneTimeSetUp]
    public void SetUpService()
    {
        _service = new UserService(_db);
    }

    [Test]
    public async Task LogIn_CorrectCredentials()
    {
        // Arrange
        Environment.SetEnvironmentVariable("Secret",
            "A key has to be long enough in order to satisfy security requirements.");
        var user = UserGenerator.Generate(1)[0];

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        var request = new LogInRequest();
        request.Password = UserGenerator.DefaultPassword;
        request.Username = user.Name;

        // Act
        Results<Ok<string>, UnauthorizedHttpResult>
            loginAttempt = await AuthenticationEndpoint.LogIn(request, _service);

        // Assert
        Assert.That(loginAttempt.Result, Is.TypeOf(typeof(Ok<string>)));

        if (loginAttempt.Result is Ok<string> result)
            Console.WriteLine(result.Value);
    }

    [Test]
    public async Task RegisterUser_ValidInput()
    {
        //Arrange
        var user = UserGenerator.Generate(1)[0];
        RegisterRequest registerRequest = new RegisterRequest();
        registerRequest.Name = user.Name;
        registerRequest.Password = UserGenerator.DefaultPassword;
        registerRequest.Email = user.Email;

        //Act
        await AuthenticationEndpoint.Register(_service, registerRequest);

        //Assert
        var createdUser = await _service.GetByName(user.Name);
        Assert.IsNotNull(createdUser);
    }
}