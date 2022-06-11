using jiraF.Member.API.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace jiraF.Member.API.Infrastructure.Data.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
#if DEBUG
        TestData(this).Seed();
#endif
    }

    public DbSet<MemberEntity> Members { get; set; }
}
