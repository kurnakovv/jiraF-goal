using jiraF.Goal.API.GlobalVariables;
using jiraF.Goal.API.Infrastructure.Data.Contexts;
using jiraF.Goal.API.Infrastructure.Data.Entities;

namespace jiraF.Goal.API.Infrastructure.Data
{
    public class TestData
    {
        private readonly AppDbContext _dbContext;

        private readonly static Guid _labelId = new("4674f93c-6331-4e63-b298-349619fa8741");
        private static List<GoalEntity> _goalEntities = new()
        {
            new GoalEntity() { Id = new Guid("a27723d9-fd4c-4b83-add8-f1c9152585ea"), AssigneeId = new Guid(DefaultMemberVariables.Id), ReporterId = new Guid(DefaultMemberVariables.Id), LabelId = _labelId, DateOfCreate = DateTime.UtcNow, Description = "Test desc1", Title = "Test title1" },
            new GoalEntity() { Id = new Guid("a27723d9-fd4c-4b83-add8-f1c9152585eb"), AssigneeId = new Guid(DefaultMemberVariables.Id), ReporterId = new Guid(DefaultMemberVariables.Id), LabelId = _labelId, DateOfCreate = DateTime.UtcNow, Description = "Test desc2", Title = "Test title2" },
            new GoalEntity() { Id = new Guid("a27723d9-fd4c-4b83-add8-f1c9152585ec"), AssigneeId = new Guid(DefaultMemberVariables.Id), ReporterId = new Guid(DefaultMemberVariables.Id), LabelId = _labelId, DateOfCreate = DateTime.UtcNow, Description = "Test desc3", Title = "Test title3" }
        };

        private static List<LabelEntity> _labelEntities = new()
        {
            new LabelEntity() { Id = new Guid("4674f93c-6331-4e63-b298-349619fa8741"), Title = "Test title1" },
            new LabelEntity() { Id = new Guid("4674f93c-6331-4e63-b298-349619fa8742"), Title = "Test title2" },
            new LabelEntity() { Id = new Guid("4674f93c-6331-4e63-b298-349619fa8743"), Title = "Test title3" }
        };

        public TestData(
            AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            try
            {
                if (!_dbContext.Goals.Any())
                {
                    _dbContext.Goals.AddRange(_goalEntities);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Labels.Any())
                {
                    _dbContext.Labels.AddRange(_labelEntities);
                    _dbContext.SaveChanges();
                }
            }
            catch(ArgumentException) { }
        }
    }
}
