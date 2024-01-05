using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using mind_your_domain;
using mind_your_domain.Database;
using mind_your_tests.Generators;

namespace mind_your_tests.Integration;

public class GroupEndpointIt
{
    private MindYourMoneyDb _db;

    [OneTimeSetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<MindYourMoneyDb>()
            .UseInMemoryDatabase(databaseName: "Testy McTestington")
            .Options;

        _db = new MindYourMoneyDb(options);
    }

    [Test]
    public async Task GetGroupById_GroupExists()
    {
        //Arrange
        var Group = GroupGenerator.Generate(1)[0];
        _db.Groups.Add(Group);
        await _db.SaveChangesAsync();

        //Act
        var endpointCall =
            GroupEndpoint.GetGroupById(Group.Id, _db).Result;

        //Assert
        Assert.IsTrue(endpointCall.Result is Ok<Group>);
        var okResult = (Ok<Group>)endpointCall.Result;
        Assert.AreEqual(Group, okResult.Value);
    }

    [Test]
    public async Task GetGroupById_GroupDoesntExist()
    {
        //Arrange
        var group = GroupGenerator.Generate(1)[0];
        _db.Groups.Add(group);
        await _db.SaveChangesAsync();

        //Act
        var endpointCall =
            GroupEndpoint.GetGroupById(Guid.NewGuid(), _db).Result;

        //Assert
        Assert.IsTrue(endpointCall.Result is NotFound);
    }


    [Test]
    public void GetAllGroups()
    {
        //Arrange
        var groups = GroupGenerator.Generate(10);
        _db.Groups.AddRange(groups);
        _db.SaveChanges();

        //Act
        List<Group>? result =
            GroupEndpoint.GetAllGroups(_db).Result.Value;

        //Assert
        Assert.IsNotNull(result);
        Assert.That(result, Is.EqualTo(groups));
    }


    [Test]
    public async Task CreateGroup_ValidInput()
    {
        //Arrange
        var group = GroupGenerator.Generate(1)[0];

        //Act
        await GroupEndpoint.CreateGroup(group, _db);

        //Assert
        var createdGroup = await _db.Groups.FindAsync(group.Id);
        Assert.IsNotNull(createdGroup);
        Assert.That(createdGroup, Is.EqualTo(group));
    }
    
    [TearDown]
    public void TearDown()
    {
        _db.RemoveRange(_db.Set<Group>());
    }
}