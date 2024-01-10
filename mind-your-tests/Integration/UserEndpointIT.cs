using Microsoft.AspNetCore.Http.HttpResults;
using mind_your_domain;
using mind_your_money_server.Database.Services;
using mind_your_tests.Utilities;

namespace mind_your_tests.Integration;

public class UserEndpointIt : DatabaseIntegrationTest<User>
{
    private UserService _service = null!;

    [OneTimeSetUp]
    public void SetUpService()
    {
        _service = new UserService(_db);
    }

    [Test]
    public async Task GetUserById_UserExists()
    {
        //Arrange
        var user = UserGenerator.Generate(1)[0];
        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        //Act
        var endpointCall =
            UserEndpoint.GetUserById(user.Id, _service).Result;

        //Assert
        Assert.IsTrue(endpointCall.Result is Ok<User>);
        var okResult = (Ok<User>)endpointCall.Result;
        Assert.That(okResult.Value, Is.EqualTo(user));
    }

    [Test]
    public async Task GetUserById_UserDoesntExist()
    {
        //Arrange
        var user = UserGenerator.Generate(1)[0];
        await _service.Create(user);

        //Act
        var endpointCall =
            UserEndpoint.GetUserById(Guid.NewGuid(), _service).Result;

        //Assert
        Assert.IsTrue(endpointCall.Result is NotFound);
    }


    [Test]
    public void GetAllUsers()
    {
        //Arrange
        var users = UserGenerator.Generate(10);
        _db.Users.AddRange(users);
        _db.SaveChangesAsync();

        //Act
        List<User>? result =
            UserEndpoint.GetAllUsers(_service).Result.Value;

        //Assert
        Assert.IsNotNull(result);
        Assert.That(result.Count, Is.EqualTo(users.Count));
    }


    [Test]
    public async Task CreateUser_ValidInput()
    {
        //Arrange
        var user = UserGenerator.Generate(1)[0];

        //Act
        await UserEndpoint.CreateUser(user, _service);

        //Assert
        var createdUser = await _service.FindById(user.Id);
        Assert.IsNotNull(createdUser);
        Assert.That(createdUser, Is.EqualTo(user));
    }

    [Test]
    public async Task DeleteUser_UserExists()
    {
        //Arrange
        var user = UserGenerator.Generate(1)[0];
        await _service.Create(user);
        //Act
        var result = await UserEndpoint.DeleteUserById(user.Id, _service);

        //Assert
        var actual = await _service.FindById(user.Id);
        Assert.That(actual, Is.Null);
        Assert.That(result.Result, Is.TypeOf(typeof(Ok)));
    }
}