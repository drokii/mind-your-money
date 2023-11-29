using Microsoft.EntityFrameworkCore;
using mind_your_money_api;
using mind_your_money_domain.db;

var builder = WebApplication.CreateBuilder(args);

// Setup Logging 
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add Services 
builder.Services.AddLogging();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add DB Context Injection (this can be cleaned up eventually)
builder.Services.AddDbContext<UserDb>(opt 
    => opt.UseInMemoryDatabase("MindYourMoney"));

// Init endpoints
var app = builder.Build();
UserApi.BuildEndpoints(app);


app.Run();