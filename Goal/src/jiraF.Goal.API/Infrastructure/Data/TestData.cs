using jiraF.Goal.API.Infrastructure.Data.Contexts;
using jiraF.Goal.API.Infrastructure.Data.Entities;

namespace jiraF.Goal.API.Infrastructure.Data
{
    public class TestData
    {
        private readonly AppDbContext _dbContext;

        private readonly static Guid _assigneeId = new("a14bea35-2311-4712-851c-744919effe3c");
        private readonly static Guid _reporterId = new("0bbde290-3050-414d-8e0a-2dadecc7cdbb");
        private readonly static Guid _labelId = new("4674f93c-6331-4e63-b298-349619fa8741");
        private static List<GoalEntity> _goalEntities = new()
        {
            new GoalEntity() { Id = new Guid("a27723d9-fd4c-4b83-add8-f1c9152585ea"), AssigneeId = _assigneeId, ReporterId = _reporterId, LabelId = _labelId, DateOfCreate = DateTime.UtcNow, Description = "Test desc1", Title = "Test title1" },
            new GoalEntity() { Id = new Guid("a27723d9-fd4c-4b83-add8-f1c9152585eb"), AssigneeId = _assigneeId, ReporterId = _reporterId, LabelId = _labelId, DateOfCreate = DateTime.UtcNow, Description = "Test desc2", Title = "Test title2" },
            new GoalEntity() { Id = new Guid("a27723d9-fd4c-4b83-add8-f1c9152585ec"), AssigneeId = _assigneeId, ReporterId = _reporterId, LabelId = _labelId, DateOfCreate = DateTime.UtcNow, Description = "Test desc3", Title = "Test title3" }
        };

        public TestData(
            AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (!_dbContext.Goals.Any())
            {
                _dbContext.Goals.AddRange(_goalEntities);
                _dbContext.SaveChanges();
            }
        }
    }
}
