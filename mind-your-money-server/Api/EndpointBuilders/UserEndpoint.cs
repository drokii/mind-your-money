using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using mind_your_domain;
using mind_your_domain.Database;

public class UserEndpoint
{
    public static void Build(WebApplication app)
    {
        app.MapGet("/users", GetAllUsers);
        app.MapGet("/users/{id}", GetUserById);
        app.MapPost("/users", CreateUser);
    }

    static async Task<Results<Ok<User>, NotFound>> GetUserById(int id, UserDb db) =>
        await db.Users.FindAsync(id) 
            is {} user // This null check looks awful, I have yet to figure out a more readable version of this.
            ? TypedResults.Ok(user)
            : TypedResults.NotFound();

    static async Task<List<User>> GetAllUsers(UserDb db) =>
        await db.Users.ToListAsync();

    static async Task<IResult> CreateUser(User user, UserDb db)
    {
        db.Users.Add(user);
        await db.SaveChangesAsync();
        return Results.Created($"/users/user/{user.Id}", user);
    }
}