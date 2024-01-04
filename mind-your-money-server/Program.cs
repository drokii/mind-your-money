using Microsoft.EntityFrameworkCore;
using mind_your_domain.Database;

var builder = WebApplication.CreateBuilder(args);

// Database Setup
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<UserDb>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

UserEndpoint.Build(app);
app.MapGet("/", () => "Hello World!");

app.Run();