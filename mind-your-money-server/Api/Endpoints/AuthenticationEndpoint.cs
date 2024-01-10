using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using mind_your_domain;
using mind_your_domain.Database;
using mind_your_domain.Database.Services;
using mind_your_money_server.Api.DTOs;
using mind_your_money_server.Database.Services;
using static mind_your_money_server.Api.Security.SecurityUtilities;

namespace mind_your_money_server.Api.Endpoints;

public static class AuthenticationEndpoint
{
    public static void Build(WebApplication app)
    {
    }

    public static async Task<Results<Ok<string>, UnauthorizedHttpResult>>
        LogIn(LogInRequest request, UserService userService)
    {
        User? userLoggingIn = await userService.GetUserByName(request.Username);

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