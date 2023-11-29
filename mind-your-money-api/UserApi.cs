using mind_your_money_domain;
using mind_your_money_domain.db;
using Microsoft.EntityFrameworkCore;

namespace mind_your_money_api;

static class UserApi
{
    private const String Endpoint = "/users";

    public static void BuildEndpoints(WebApplication app)
    { 
        app.MapGet(Endpoint, async (UserDb db) => 
            await db.Users.ToListAsync());

        app.MapPost(Endpoint,
            async (User inputUser, UserDb db) =>
            {
                db.Users.Add(inputUser);
                await db.SaveChangesAsync();

                return Results.Created($"/users/{inputUser.Id}", inputUser);
            });
        
    }
}
