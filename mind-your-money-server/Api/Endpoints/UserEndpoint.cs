using Microsoft.AspNetCore.Http.HttpResults;
using mind_your_domain;
using mind_your_money_server.Database.Services;
using static Microsoft.AspNetCore.Http.TypedResults;

public static class UserEndpoint
{
    public static void Build(WebApplication app)
    {
        app.MapGet("/users", GetAllUsers).WithOpenApi();
        app.MapGet("/users/{id}", GetUserById).WithOpenApi();
        app.MapPost("/users", CreateUser).WithOpenApi();
        app.MapDelete("/users/{id}", DeleteUserById).WithOpenApi();
    }

    public static async Task<Results<Ok<User>, NotFound>> GetUserById(Guid id, UserService userService)
    {
        var user = await userService.FindById(id);
        if (user is null)
            return NotFound();

        return Ok(user);
    }


    public static async Task<Ok<List<User>>> GetAllUsers(UserService userService)
    {
        var users = await userService.GetAll();
        return Ok(users);
    }

    public static async Task<IResult> CreateUser(User user, UserService userService)
    {
        await userService.Create(user);
        return Created();
    }

    public static async Task<Results<Ok, NotFound>> DeleteUserById(Guid userId, UserService userService)
    {
        var userToBeRemoved = await userService.FindById(userId);

        if (userToBeRemoved is null)
            return NotFound();

        await userService.Remove(userToBeRemoved);
        return Ok();
    }
}