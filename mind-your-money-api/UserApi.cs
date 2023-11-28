using System.Diagnostics;
using mind_your_money_domain;
using mind_your_money_domain.db;
using Microsoft.EntityFrameworkCore;

namespace mind_your_money_api;

class UserApi
{
    protected UserApi(){}
    
    public static void BuildEndpoints(WebApplication app)
    {
        app.MapGet("/users", async (UserDb db) =>
            await db.UserDbs.ToListAsync());

        app.MapPost("/users/{id}",
            async (int id, User inputUser, UserDb db) =>
            {
                var user = await db.UserDbs.FindAsync(id);

                user.Name = inputUser.Name;
                
                Console.WriteLine("Creating User.");

                await db.SaveChangesAsync();
                
                return Results.NoContent();
            });
    }
}