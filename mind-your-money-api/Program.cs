using Microsoft.EntityFrameworkCore;
using mind_your_money_domain.db;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<UserDb>(opt => opt.UseInMemoryDatabase("MindYourMoney"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();