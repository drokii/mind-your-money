namespace mind_your_domain.Database;

using Microsoft.EntityFrameworkCore;

public class MindYourMoneyDb : DbContext
{
    public MindYourMoneyDb(DbContextOptions<MindYourMoneyDb> options)
        : base(options) {}

    public DbSet<User> Users => Set<User>();
    public DbSet<Group> Groups => Set<Group>();

}