using jiraF.User.API.Infrastructure.Data.Contexts;
using jiraF.User.API.Infrastructure.Data.Entities;

namespace jiraF.User.API.Infrastructure.Data;

public class TestData
{
    private readonly AppDbContext _dbContext;

    private static List<UserEntity> _userEntities = new()
    {
        new UserEntity() { Id = new Guid("2f857708-6e97-413b-b495-f2161135616a"), DateOfRegistration = DateTime.UtcNow, Name = "FirstName" },
        new UserEntity() { Id = new Guid("2f857708-6e97-413b-b495-f2161135616b"), DateOfRegistration = DateTime.UtcNow, Name = "SecondName" },
        new UserEntity() { Id = new Guid("2f857708-6e97-413b-b495-f2161135616c"), DateOfRegistration = DateTime.UtcNow, Name = "ThirdName" }
    };

    public TestData(
        AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Seed()
    {
        if (!_dbContext.Users.Any())
        {
            _dbContext.Users.AddRange(_userEntities);
            _dbContext.SaveChanges();
        }
    }
}
