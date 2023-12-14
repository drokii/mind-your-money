using Microsoft.EntityFrameworkCore;
using mind_your_money_domain.db;

namespace mind_your_money_api;

public static class MindYourMoneyServer
{
    public static void Start()
    {
        var builder = WebApplication.CreateBuilder();

        SetUpLogging(builder);
        SetUpServices(builder);
        
        var app = builder.Build();
        
        SetUpEndpoints(app);

        app.Run();
    }

    private static void SetUpEndpoints(WebApplication app)
    {
        UserApi.BuildEndpoints(app);
    }

    private static void SetUpServices(WebApplicationBuilder builder)
    {
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        // Add DB Context Injection (this can be cleaned up eventually)
        builder.Services.AddDbContext<UserDb>(opt
            => opt.UseInMemoryDatabase("MindYourMoney"));
    }

    private static void SetUpLogging(WebApplicationBuilder builder)
    {
        builder.Logging.AddConsole();
        builder.Logging.AddDebug();
        builder.Services.AddLogging();
    }
}