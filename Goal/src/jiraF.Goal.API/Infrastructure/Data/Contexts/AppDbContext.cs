using jiraF.Goal.API.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace jiraF.Goal.API.Infrastructure.Data.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
    {
#if DEBUG
        new TestData(this).Seed();
#endif
    }

    public DbSet<GoalEntity> Goals { get; set; }
    public DbSet<LabelEntity> Labels { get; set; }
}
