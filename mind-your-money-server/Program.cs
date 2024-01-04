using Microsoft.EntityFrameworkCore;
using mind_your_domain.Database;

var builder = WebApplication.CreateBuilder(args);

// Database Setup
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<MindYourMoneyDb>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

UserEndpoint.Build(app);
GroupEndpoint.Build(app);


app.MapGet("/", () => "Hello World!");

app.Run();