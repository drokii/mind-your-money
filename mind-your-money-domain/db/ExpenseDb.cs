using Microsoft.EntityFrameworkCore;

namespace mind_your_money_domain.db;

public class ExpenseDb : DbContext
{
    public ExpenseDb(DbContextOptions<UserDb> options)
        : base(options) {}

    public DbSet<UserDb> ExpenseDbs => Set<UserDb>();
}