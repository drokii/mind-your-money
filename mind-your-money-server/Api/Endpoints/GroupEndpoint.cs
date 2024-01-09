using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using mind_your_domain;
using mind_your_domain.Database;
using static Microsoft.AspNetCore.Http.TypedResults;

public static class GroupEndpoint
{
    public static void Build(WebApplication app)
    {
        app.MapGet("/groups", GetAllGroups);
        app.MapGet("/groups/{id}", GetGroupById);
        app.MapPost("/groups", CreateGroup);
        app.MapPut("/groups/addUser", AddUserToGroup);
    }

    public static async Task<Results<Ok<Group>, NotFound>> GetGroupById(Guid id, MindYourMoneyDb db) =>
        await db.Groups.FindAsync(id) 
            is {} group 
            ? Ok(group)
            : NotFound(); 

    public static async Task<Ok<List<Group>>> GetAllGroups(MindYourMoneyDb db)
    {
        var groups = await db.Groups.ToListAsync();
        return Ok(groups);
    }

    public static async Task<IResult> CreateGroup(Group group, MindYourMoneyDb db)
    {
        db.Groups.Add(group);
        await db.SaveChangesAsync();
        return Created($"/groups/group/{group.Id}", group);
    }

    public static async Task<Results<Ok, Conflict, NotFound>> AddUserToGroup(Guid userId, Guid groupId, MindYourMoneyDb db)
    {
        Group? targetGroup = await db.Groups.FindAsync(groupId);
        User? targetUser = await db.Users.FindAsync(userId);

        if (targetGroup is null || targetUser is null)
            return NotFound();
        
        bool userIsAlreadyInGroup = targetGroup.Members.Exists(user => user.Id.Equals(userId));

        if (userIsAlreadyInGroup)
            return Conflict();
        
        return Ok();
    }


    public static async Task<Results<Ok, NotFound>> DeleteGroupById(Guid groupId, MindYourMoneyDb db)
    {
        Group? groupToBeRemoved = await db.Groups.FindAsync(groupId);

        if (groupToBeRemoved is null)
            return NotFound();

        db.Groups.Remove(groupToBeRemoved);
        await db.SaveChangesAsync();
        return Ok();
    }
}