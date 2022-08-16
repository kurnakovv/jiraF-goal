using jiraF.User.API.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace jiraF.User.API.Infrastructure.Data.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
#if DEBUG
        new TestData(this).Seed();
#endif
    }

    public DbSet<UserEntity> Users { get; set; }
}
