using Microsoft.AspNetCore.Http.HttpResults;
using mind_your_domain;
using mind_your_money_server.Api.Endpoints;
using mind_your_money_server.Database.Services;
using mind_your_tests.Generators;
using mind_your_tests.Utilities;

namespace mind_your_tests.Integration;

public class GroupEndpointIt : DatabaseIntegrationTest<Group>
{
    private GroupService? _service;

    [OneTimeSetUp]
    public void SetUpService()
    {
        _service = new GroupService(_db);
    }

    [Test]
    public async Task GetGroupById_GroupExists()
    {
        //Arrange
        var group = GroupGenerator.Generate(1)[0];
        _db.Groups.Add(group);
        await _db.SaveChangesAsync();

        //Act
        var endpointCall =
            GroupEndpoint.GetGroupById(group.Id, _service).Result;

        //Assert
        Assert.IsTrue(endpointCall.Result is Ok<Group>);
        var okResult = (Ok<Group>)endpointCall.Result;
        Assert.That(okResult.Value, Is.EqualTo(group));
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
            GroupEndpoint.GetGroupById(Guid.NewGuid(), _service).Result;

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
        var result =
            GroupEndpoint.GetAllGroups(_service).Result.Value;

        //Assert
        Assert.IsNotNull(result);
        Assert.That(result.Count, Is.EqualTo(groups.Count));
    }


    [Test]
    public async Task CreateGroup_ValidInput()
    {
        //Arrange
        var group = GroupGenerator.Generate(1)[0];

        //Act
        await GroupEndpoint.CreateGroup(group, _service);

        //Assert
        var createdGroup = await _db.Groups.FindAsync(group.Id);
        Assert.IsNotNull(createdGroup);
        Assert.That(createdGroup, Is.EqualTo(group));
    }

    [Test]
    public async Task DeleteGroup_GroupExists()
    {
        //Arrange
        var group = GroupGenerator.Generate(1)[0];
        _db.Groups.Add(group);
        await _db.SaveChangesAsync();

        //Act
        await GroupEndpoint.DeleteGroupById(group.Id, _service);

        //Assert
        var actual = _db.Groups.FindAsync(group.Id).Result;
        Assert.That(actual, Is.Null);
    }
}