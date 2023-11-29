using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<UserDb>(opt => opt.UseInMemoryDatabase("MindYourMoney"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();