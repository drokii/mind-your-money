using mind_your_domain.Database.Services;
using mind_your_money_server.Database.Services;

namespace mind_your_money_server.Configuration;

public static class DependencyRegistry
{
    public static void RegisterDependencies(WebApplicationBuilder builder)
    {
        // As the app grows, I might, or not, add the interfaces.
        // I generally disagree with the need for full blown OOP for simple things.
        builder.Services.AddScoped<UserService>();
    }
    
}