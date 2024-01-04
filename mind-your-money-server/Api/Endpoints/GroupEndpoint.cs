using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mind_your_domain;
using mind_your_domain.Database;

public class GroupEndpoint
{
    public static void Build(WebApplication app)
    {
        app.MapGet("/groups", GetAllGroups);
        app.MapGet("/groups/{id}", GetGroupById);
        app.MapPost("/groups", CreateGroup);
    }

    static async Task<Results<Ok<Group>, NotFound>> GetGroupById(Guid id, MindYourMoneyDb db) =>
        await db.Groups.FindAsync(id) 
            is {} group 
            ? TypedResults.Ok(group)
            : TypedResults.NotFound(); 

    static async Task<List<Group?>> GetAllGroups(MindYourMoneyDb db) =>
        await db.Groups.ToListAsync();

    static async Task<IResult> CreateGroup(Group? group, MindYourMoneyDb db)
    {
        db.Groups.Add(group);
        await db.SaveChangesAsync();
        return TypedResults.Created($"/groups/group/{group.Id}", group);
    }

    static async Task<Results<Ok, Conflict, NotFound>> AddUserToGroup(Guid userId, Guid groupId, MindYourMoneyDb db)
    {
        Group? targetGroup = await db.Groups.FindAsync(groupId);
        User? targetUser = await db.Users.FindAsync(userId);

        if (targetGroup is null || targetUser is null)
            return TypedResults.NotFound();
        
        bool userIsAlreadyInGroup = targetGroup.Members.Exists(user => user.Id.Equals(userId));

        if (userIsAlreadyInGroup)
            return TypedResults.Conflict();
        
        return TypedResults.Ok();
    }
    
}