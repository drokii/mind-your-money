using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using mind_your_domain;
using mind_your_domain.Database;
using mind_your_money_server.Api.DTOs;

namespace mind_your_money_server.Api.Endpoints;

public static class AuthenticationEndpoint
{
    public static void Build(WebApplication app)
    {
    }

    public static async Task<Results<Ok<string>, UnauthorizedHttpResult>>
        LogIn(LogInRequest request, MindYourMoneyDb db)
    {
        User? userLoggingIn = await GetUserByName(request, db);

        if (userLoggingIn == null)
            return TypedResults.Unauthorized();
        return null;
    }

    private static async Task<User?> GetUserByName(LogInRequest request, MindYourMoneyDb db)
    {
        return await db.Users
            .FirstOrDefaultAsync(user =>
                user.Name.Equals(request.Username));
    }
}