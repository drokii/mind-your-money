using Microsoft.AspNetCore.Http.HttpResults;
using mind_your_domain;
using mind_your_money_server.Api.DTOs;
using mind_your_money_server.Database.Services;
using static mind_your_money_server.Api.Security.SecurityUtilities;

namespace mind_your_money_server.Api.Endpoints;

public static class AuthenticationEndpoint
{
    public static void Build(WebApplication app)
    {
        app.MapGet("/auth/login", LogIn).WithOpenApi();
        app.MapPost("/auth/register", Register).WithOpenApi();
    }

    public static async Task<Results<BadRequest<string>, Created>> Register(UserService userService,
        RegisterRequest registerRequest)
    {
        var user = await userService.GetByName(registerRequest.Name);

        if (user is not null)
            return TypedResults.BadRequest("Username has already been taken. Sorry!");

        await SafelyCreateUser(userService, registerRequest);

        return TypedResults.Created();
    }

    private static async Task SafelyCreateUser(UserService userService, RegisterRequest registerRequest)
    {
        var salt = GenerateSalt();
        var hashedPassword = Hash(registerRequest.Password, salt);

        await userService.Create(
            new User(
                registerRequest.Name,
                registerRequest.Email,
                hashedPassword,
                salt)
        );
    }

    public static async Task<Results<Ok<string>, UnauthorizedHttpResult>>
        LogIn(LogInRequest request, UserService userService)
    {
        User? userLoggingIn = await userService.GetByName(request.Username);

        if (userLoggingIn == null)
            return TypedResults.Unauthorized();

        if (VerifyPassword(
                request.Password,
                userLoggingIn.Password,
                userLoggingIn.Salt))
            return TypedResults.Ok(GenerateToken(userLoggingIn));

        return TypedResults.Unauthorized();
    }
}