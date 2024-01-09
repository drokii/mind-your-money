using Microsoft.EntityFrameworkCore;
using mind_your_domain.Database;

var builder = WebApplication.CreateBuilder(args);

// Database Setup
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<MindYourMoneyDb>(options => options.UseNpgsql(connectionString));

// Auth Setup
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

// Swagger Setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// More Swagger Setup
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


UserEndpoint.Build(app);
GroupEndpoint.Build(app);


app.MapGet("/", () => "Hello World!");

app.Run();