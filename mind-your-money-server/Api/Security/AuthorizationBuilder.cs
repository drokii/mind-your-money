using mind;

namespace mind_your_money_server.Api.Security;

public static class AuthorizationBuilder
{
    public static void Build(WebApplicationBuilder builder)
    {
        AddPolicy(builder, Policies.Admin, Roles.Admin);
        AddPolicy(builder, Policies.User, Roles.User);
        AddPolicy(builder, Policies.All, null);
    }

    private static void AddPolicy(WebApplicationBuilder builder, Policies policy, Roles? role)
    {
        if (role != null)
            builder.Services.AddAuthorizationBuilder()
                .AddPolicy(policy.ToString(), p => p.RequireRole(role.ToString()));
    }
}