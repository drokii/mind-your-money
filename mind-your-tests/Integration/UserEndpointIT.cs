using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using mind_your_domain;
using mind_your_domain.Database;
using mind_your_tests.Generators;

namespace mind_your_tests.Integration;

public class UserEndpointIt
{
    private MindYourMoneyDb _db;

    [OneTimeSetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<MindYourMoneyDb>()
            .UseInMemoryDatabase(databaseName: "Testie McTestington")
            .Options;

        _db = new MindYourMoneyDb(options);
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
             UserEndpoint.GetUserById(user.Id, _db).Result;

        //Assert
        Assert.IsTrue(endpointCall.Result is Ok<User>);
        var okResult = (Ok<User>) endpointCall.Result;
        Assert.That(okResult.Value, Is.EqualTo(user));
    }
    
    [Test]
    public async Task GetUserById_UserDoesntExist()
    {
        //Arrange
        var user = UserGenerator.Generate(1)[0];
        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        //Act
        var endpointCall = 
            UserEndpoint.GetUserById(Guid.NewGuid(), _db).Result;

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
            UserEndpoint.GetAllUsers(_db).Result.Value;

        //Assert
        Assert.IsNotNull(result);
        Assert.That(result, Is.EqualTo(users));
    }
    
     
    [Test]
    public async Task CreateUser_ValidInput()
    {
        //Arrange
        var user = UserGenerator.Generate(1)[0];

        //Act
        await UserEndpoint.CreateUser(user, _db);

        //Assert
        var createdUser = await _db.Users.FindAsync(user.Id);
        Assert.IsNotNull(createdUser);
        Assert.That(createdUser, Is.EqualTo(user));
    }

    [TearDown]
    public void TearDown()
    {
        _db.RemoveRange(_db.Set<User>());
    }
    
    
}