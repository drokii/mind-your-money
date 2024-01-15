using Microsoft.AspNetCore.Http.HttpResults;
using mind_your_domain;
using mind_your_money_server.Database.Services;
using static Microsoft.AspNetCore.Http.TypedResults;

public static class GroupEndpoint
{
    public static void Build(WebApplication app)
    {
        app.MapGet("/groups", GetAllGroups).WithOpenApi();
        app.MapGet("/groups/{id}", GetGroupById).WithOpenApi();
        app.MapPost("/groups", CreateGroup).WithOpenApi();
        app.MapPut("/groups/addUser", AddUserToGroup).WithOpenApi();
        app.MapDelete("/group/{id}", DeleteGroupById).WithOpenApi();
    }

    public static async Task<Results<Ok<Group>, NotFound>> GetGroupById(Guid id, GroupService? service) =>
        await service.GetById(id)
            is { } group
            ? Ok(group)
            : NotFound();

    public static async Task<Ok<List<Group>>> GetAllGroups(GroupService service)
    {
        var groups = await service.GetAll();
        return Ok(groups);
    }

    public static async Task<IResult> CreateGroup(Group group, GroupService service)
    {
        await service.Create(group);
        return Created();
    }

    public static async Task<Results<Ok, Conflict, NotFound>> AddUserToGroup(Guid userId, Guid groupId,
        GroupService groupService, UserService userService)
    {
        Group? targetGroup = await groupService.GetById(groupId);
        User? targetUser = await userService.GetById(userId);

        if (targetGroup is null || targetUser is null)
            return NotFound();

        bool userIsAlreadyInGroup =
            targetGroup.Members.Exists(
                user => user.Id.Equals(userId)
            );

        if (userIsAlreadyInGroup)
            return Conflict();

        return Ok();
    }

    public static async Task<Results<Ok, NotFound>> DeleteGroupById(Guid groupId, GroupService service)
    {
        Group? groupToBeRemoved = await service.GetById(groupId);

        if (groupToBeRemoved is null)
            return NotFound();

        await service.Remove(groupToBeRemoved);
        return Ok();
    }
}