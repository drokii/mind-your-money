using Microsoft.AspNetCore.Http.HttpResults;
using mind;
using mind_your_domain;
using mind_your_money_server.Database.Services;
using static Microsoft.AspNetCore.Http.TypedResults;

namespace mind_your_money_server.Api.Endpoints;

public static class UserEndpoint
{
    public static void Build(WebApplication app)
    {
        app.MapGet("/users", GetAllUsers).WithOpenApi().RequireAuthorization(Policies.User.ToString());
        app.MapGet("/users/{id}", GetUserById).WithOpenApi().RequireAuthorization(Policies.User.ToString());
        app.MapDelete("/users/{id}", DeleteUserById).WithOpenApi().RequireAuthorization(Policies.User.ToString());
    }

    public static async Task<Results<Ok<User>, NotFound>> GetUserById(Guid id, UserService userService)
    {
        var user = await userService.GetById(id);
        if (user is null)
            return NotFound();

        return Ok(user);
    }


    public static async Task<Ok<List<User>>> GetAllUsers(UserService userService)
    {
        var users = await userService.GetAll();
        return Ok(users);
    }

    public static async Task<Results<Ok, NotFound>> DeleteUserById(Guid userId, UserService userService)
    {
        var userToBeRemoved = await userService.GetById(userId);

        if (userToBeRemoved is null)
            return NotFound();

        await userService.Remove(userToBeRemoved);
        return Ok();
    }
}