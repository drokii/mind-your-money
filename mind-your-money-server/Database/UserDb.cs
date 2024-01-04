namespace mind_your_domain.Database;

using Microsoft.EntityFrameworkCore;

public class UserDb : DbContext
{
    public UserDb(DbContextOptions<UserDb> options)
        : base(options) {}

    public DbSet<User> Users => Set<User>();
}