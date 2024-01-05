using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using mind_your_domain;
using mind_your_domain.Database;

public static class UserEndpoint
{
    public static void Build(WebApplication app)
    {
        app.MapGet("/users", GetAllUsers);
        app.MapGet("/users/{id}", GetUserById);
        app.MapPost("/users", CreateUser);
    }

    public static async Task<Results<Ok<User>, NotFound>> GetUserById(Guid id, MindYourMoneyDb db) =>
        await db.Users.FindAsync(id)
            is { } user // This null check looks awful, I have yet to figure out a more readable version of this.
            ? TypedResults.Ok(user)
            : TypedResults.NotFound();

    public static async Task<Ok<List<User>>> GetAllUsers(MindYourMoneyDb db)
    {
        var users = await db.Users.ToListAsync();
        return TypedResults.Ok(users);
    }

    public static async Task<IResult> CreateUser(User user, MindYourMoneyDb db)
    {
        db.Users.Add(user);
        await db.SaveChangesAsync();
        return Results.Created($"/users/user/{user.Id}", user);
    }
}