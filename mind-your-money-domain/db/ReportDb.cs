using Microsoft.EntityFrameworkCore;

namespace mind_your_money_domain.db;

public class ReportDb : DbContext
{
    public ReportDb(DbContextOptions<ReportDb> options)
        : base(options)
    {
    }

    public DbSet<ReportDb> Reports => Set<ReportDb>();
}