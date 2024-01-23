using Microsoft.EntityFrameworkCore;
using mind_your_domain.Database;
using mind_your_money_server.Api.Endpoints;
using mind_your_money_server.Api.Security;
using mind_your_money_server.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Database Setup
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<MindYourMoneyDb>(options => options.UseNpgsql(connectionString));

// Dependency Setup
DependencyRegistry.RegisterDependencies(builder);

// Auth Setup
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();
AuthorizationBuilder.Build(builder);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000/",
                "http://www.contoso.com");
        });
});

// Swagger Setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

AuthenticationEndpoint.Build(app);
UserEndpoint.Build(app);
GroupEndpoint.Build(app);

app.UseCors(MyAllowSpecificOrigins);
app.Run();