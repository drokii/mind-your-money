using Microsoft.EntityFrameworkCore;

namespace mind_your_money_domain.db;

public class UserDb : DbContext
{
    public UserDb(DbContextOptions<UserDb> options)
        : base(options) {}

    public DbSet<User> UserDbs => Set<User>();
}