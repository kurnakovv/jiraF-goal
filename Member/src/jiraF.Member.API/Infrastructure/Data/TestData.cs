using jiraF.Member.API.Infrastructure.Data.Contexts;
using jiraF.Member.API.Infrastructure.Data.Entities;

namespace jiraF.Member.API.Infrastructure.Data;

public class TestData
{
    private readonly AppDbContext _dbContext;

    private static List<MemberEntity> _memberEntities = new()
    {
        new MemberEntity() { Id = new Guid("2f857708-6e97-413b-b495-f2161135616a"), DateOfRegistration = DateTime.UtcNow, Name = "FirstName" },
        new MemberEntity() { Id = new Guid("2f857708-6e97-413b-b495-f2161135616b"), DateOfRegistration = DateTime.UtcNow, Name = "SecondName" },
        new MemberEntity() { Id = new Guid("2f857708-6e97-413b-b495-f2161135616c"), DateOfRegistration = DateTime.UtcNow, Name = "ThirdName" }
    };

    public TestData(
        AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Seed()
    {
        if (!_dbContext.Members.Any())
        {
            _dbContext.Members.AddRange(_memberEntities);
        }
    }
}
